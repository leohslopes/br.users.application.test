using br.users.application.test.domain.Entities;
using br.users.application.test.domain.Entities.Messasing;
using ClosedXML.Excel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mysqlx.Crud;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace br.users.application.test.messasing
{
    public class WorkMessage : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<WorkMessage> _logger;
        private IConnection _connection;
        private IModel _channel;
        private readonly EmailSettings _emailSettings;
        private readonly List<UserDTO> dtos = new List<UserDTO>();
        private readonly string _excelOutputPath = Path.Combine(Directory.GetCurrentDirectory(), "output", "relatorioLogFila.xlsx");

        public WorkMessage(IConfiguration configuration,ILogger<WorkMessage> logger, IOptions<EmailSettings> emailOptions)
        {
            _configuration = configuration;
            _logger = logger;
            _emailSettings = emailOptions.Value;

            var factory = new ConnectionFactory() {
                HostName = _configuration["RabbitMQ:HostName"],
                Port = int.Parse(_configuration["RabbitMQ:Port"]),
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "user_created", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var user = JsonSerializer.Deserialize<UserDTO>(message);
                _logger.LogInformation($"👤 Usuário recebido: {user?.UserName} - {user?.UserEmail} - {user?.DatePublisher}");


                string subject = "Bem-vindo!";
                string content = $"Olá {user?.UserName}, seu cadastro foi recebido em {user?.DatePublisher}.";

                if (user != null)
                {
                    dtos.Add(user);
                    _logger.LogInformation("📥 Mensagem recebida: {@UserDTO}", user);

                    // Grava no arquivo de log
                    await GenerateReportLogAsync();
                }

                await SendEmailAsync(_configuration["AdminEmail"].ToString(), subject, content);
            };

            _channel.BasicConsume(queue: "user_created", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task GenerateReportLogAsync()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_excelOutputPath)!);

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Usuários");

                // Cabeçalhos
                worksheet.Cell(1, 1).Value = "NOME";
                worksheet.Cell(1, 2).Value = "EMAIL";
                worksheet.Cell(1, 3).Value = "DATA DE CADASTRO";

                for (int i = 0; i < dtos.Count; i++)
                {
                    var user = dtos[i];
                    worksheet.Cell(i + 2, 1).Value = user.UserName;
                    worksheet.Cell(i + 2, 2).Value = user.UserEmail;
                    worksheet.Cell(i + 2, 3).Value = user.DatePublisher.ToString("dd/MM/yyyy HH:mm:ss");
                }

                int totalRows = dtos.Count + 1;
                var tableRange = worksheet.Range(1, 1, totalRows, 3);
                var table = tableRange.CreateTable();
                table.Theme = XLTableTheme.TableStyleMedium9;


                workbook.SaveAs(_excelOutputPath);
                _logger.LogInformation("📄 Arquivo Excel gerado com sucesso em: {path}", _excelOutputPath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Erro ao gerar arquivo Excel.");
            }

            await Task.CompletedTask;
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailMessage = new MimeKit.MimeMessage();
            emailMessage.From.Add(new MimeKit.MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));
            emailMessage.To.Add(new MimeKit.MailboxAddress("", toEmail));
            emailMessage.Subject = subject;

            emailMessage.Body = new MimeKit.TextPart("plain") { Text = body };
            using var client = new MailKit.Net.Smtp.SmtpClient
            {
                CheckCertificateRevocation = Convert.ToBoolean(_configuration["CheckCertificateRevocation"])
            };

            var builder = new MimeKit.BodyBuilder
            {
                TextBody = body
            };

            // Adiciona o anexo (relatório Excel gerado)
            if (File.Exists(_excelOutputPath))
            {
                builder.Attachments.Add(_excelOutputPath);
            }


            try
            {
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.FromEmail, _emailSettings.Password);
                await client.SendAsync(emailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar e-mail.");
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
