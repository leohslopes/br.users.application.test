using br.users.application.test.application.Validators;
using br.users.application.test.domain.Entities.Achive;
using br.users.application.test.domain.Entities.UserCx;
using br.users.application.test.domain.Interfaces.Messaging;
using br.users.application.test.domain.Interfaces.Repositories;
using br.users.application.test.domain.Interfaces.Services;
using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.application.Services
{
    public class ArchiveService : BaseService, IArchiveService
    {
        private readonly ILogger<ArchiveService> _logger;
        private readonly IArchiveRepository _archiveRepository;
        private readonly IMessageBusService _messageBusService;
       
        public ArchiveService(ILogger<ArchiveService> logger, IArchiveRepository archiveRepository, IMessageBusService messageBusService)
        {
           _logger = logger;
           _archiveRepository = archiveRepository;
           _messageBusService = messageBusService;
        }

        public async Task<ResultSetImportArchive> ImportMassiveUsersData(IFormFile file)
        {
            ResultSetImportArchive resultSet = new();

            try
            {
                var validator = new UserModelValidator();
                using var reader = new StreamReader(file.OpenReadStream());
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                });
                var results = new List<ImportUser>();

                await foreach (var row in csv.GetRecordsAsync<ImportUser>())
                {
                    var result = new ImportUser
                    {
                        UserOfficialNumber = row.UserOfficialNumber,
                        UserName = row.UserName,
                        UserEmail = row.UserEmail,
                        UserPassword = row.UserPassword,
                        UserGender = row.UserGender,
                        UserAge = row.UserAge
                    };

                    var validation = validator.Validate(row);

                    if (!validation.IsValid)
                    {
                        result.Status = false;
                        result.FinalResult = string.Join(" | ", validation.Errors.Select(e => e.ErrorMessage));
                    }
                    else
                    {
                        result.Status = true;
                        result.FinalResult = "Usuário inserido com sucesso.";
                    }

                    results.Add(result);
                }

                foreach (var item in results.Where(x => x.Status.Value).ToList())
                {
                    int res = 0;

                    res = await _archiveRepository.GetUserOfficialNumberExists(item.UserOfficialNumber);
                    if (res > 0)
                    {
                        item.Status = false;
                        item.FinalResult = "CPF já cadastrado.";
                        continue;
                    }

                    res = 0;
                    res = await _archiveRepository.GetUserEmailExists(item.UserEmail);
                    if (res > 0)
                    {
                        item.Status = false;
                        item.FinalResult = "E-mail já cadastrado.";
                        continue;
                    }
                }

                if (results != null || results.Count > 0)
                {
                    var users = new List<Users>();

                    results.ForEach(item =>
                    {
                        if ((bool)item.Status)
                        {
                            users.Add(new()
                            {
                                UserID = 0,
                                UserName = item.UserName,
                                UserEmail = item.UserEmail,
                                UserAge = item.UserAge.Value,
                                UserGender = item.UserGender,
                                UserPassword = item.UserPassword,
                                UserPicture = null,
                                UserOfficialNumber = item.UserOfficialNumber,
                                DateAlter = DateTime.Now
                            });
                        }
                    });

                    if (users == null || users.Count <= 0)
                    {
                        _logger.LogInformation("Gerando a planilha de retorno do input massivo.");
                        var fileBytes = GenerateFinalResultsArchive(results).FileContents;
                        var base64 = Convert.ToBase64String(fileBytes);

                        resultSet.ResultFileContent = base64;
                        resultSet.CountRows = 0;
                        _logger.LogInformation("Planilha de retorno do input massivo gerado com sucesso.");
                    }
                    else
                    {
                        _logger.LogInformation($"Inserindo os usuários massivamente na tabela USERS_CX. Total: {users.Count}");
                        bool valid = await _archiveRepository.InsertAllUsers(users);
                        _logger.LogInformation($"Inserção massiva dos usuários na tabela USERS_CX feita com sucesso. Total: {users.Count}");

                        _logger.LogInformation($"Inserindo os usuários massivamente na fila do RabbitMQ. Total: {users.Count}");
                        users.ForEach(x =>
                        {
                            _messageBusService.PublishMessage(new domain.Entities.Messasing.UserDTO { UserEmail = x.UserEmail, UserName = x.UserName, DatePublisher = DateTime.Now });
                        });
                        _logger.LogInformation($"Inserção massiva dos usuários na fila do RabbitMQ feita com sucesso Total: {users.Count}");

                        if (valid)
                        {
                            _logger.LogInformation("Gerando a planilha de retorno do input massivo.");
                            var fileBytes = GenerateFinalResultsArchive(results).FileContents;
                            var base64 = Convert.ToBase64String(fileBytes);

                            resultSet.ResultFileContent = base64;
                            resultSet.CountRows = results.Count(predicate: static x => x.Status.Value);
                            _logger.LogInformation("Planilha de retorno do input massivo gerado com sucesso.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ImportMassiveUsersData] - Erro ao importar o massivo de usuários: {ex.Message}");
                throw ex;
            }

            return resultSet;

        }

        public async Task<string> ExportReportLogUsersData()
        {
            string base64 = string.Empty;

            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "output", "relatorioLogFila.xlsx");

                if (!File.Exists(filePath))
                {
                    _logger.LogInformation($"Relatório de log não encontrado no servidor.");
                    throw new ApplicationException($"Relatório de log não encontrado no servidor.");
                }

                _logger.LogInformation("Exportando o relatório de log do servidor.");
                var fileBytes = File.ReadAllBytes(filePath);
                base64 = Convert.ToBase64String(fileBytes);
                _logger.LogInformation("Relatório de log exportado com sucesso do servidor.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ExportReportLogUsersData] - Erro ao exportar o relatório de log: {ex.Message}");
                throw ex;
            }

            return base64;

        }

        public async Task<bool> DeleteReportFileServer()
        {
            bool result = false;

            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "output", "relatorioLogFila.xlsx");

                if (!File.Exists(filePath))
                {
                    _logger.LogInformation("Tentativa de deletar arquivo inexistente: {FilePath}", filePath);
                    throw new ApplicationException($"Tentativa de deletar arquivo inexistente: {filePath}");
                }

                File.Delete(filePath);
                _logger.LogInformation("Arquivo deletado com sucesso: {FilePath}", filePath);

                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[DeleteReportFileServer] - Erro ao deletar o arquivo físico do servidor: {ex.Message}");
                throw ex;
            }

            return result;
        }

        private FileContentResult GenerateFinalResultsArchive(List<ImportUser> results)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Resultado");

            worksheet.Cell(1, 1).Value = "CPF";
            worksheet.Cell(1, 2).Value = "NOME";
            worksheet.Cell(1, 3).Value = "EMAIL";
            worksheet.Cell(1, 4).Value = "SENHA";
            worksheet.Cell(1, 5).Value = "SEXO";
            worksheet.Cell(1, 6).Value = "IDADE";
            worksheet.Cell(1, 7).Value = "RESULTADO";

            for (int i = 0; i < results.Count; i++)
            {
                var r = results[i];
                worksheet.Cell(i + 2, 1).Value = r.UserOfficialNumber;
                worksheet.Cell(i + 2, 2).Value = r.UserName;
                worksheet.Cell(i + 2, 3).Value = r.UserEmail;
                worksheet.Cell(i + 2, 4).Value = r.UserPassword;
                worksheet.Cell(i + 2, 5).Value = r.UserGender;
                worksheet.Cell(i + 2, 6).Value = r.UserAge;
                worksheet.Cell(i + 2, 7).Value = r.FinalResult;
            }

            int totalRows = results.Count + 1;
            var tableRange = worksheet.Range(1, 1, totalRows, 7);
            var table = tableRange.CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium9;

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return new FileContentResult(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "resultado-importacao.xlsx"
            };
        }
    }
}
