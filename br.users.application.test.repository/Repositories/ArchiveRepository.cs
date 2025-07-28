using br.users.application.test.domain.Entities;
using br.users.application.test.domain.Entities.Achive;
using br.users.application.test.domain.Entities.UserCx;
using br.users.application.test.domain.Interfaces.Repositories;
using br.users.application.test.repository.Databases.Interfaces;
using br.users.application.test.repository.Repositories.SQLStatement;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace br.users.application.test.repository.Repositories
{
    public class ArchiveRepository : IArchiveRepository
    {
        private readonly ILogger<ArchiveRepository> _logger;
        private readonly IDbMySQLSession _dbMySQLSession;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        public ArchiveRepository(IConfiguration configuration, ILogger<ArchiveRepository> logger, IDbMySQLSession dbMySQLSession, AppSettings appSettings)
        {
            _logger = logger;
            _configuration = configuration;
            _dbMySQLSession = dbMySQLSession;
            _appSettings = appSettings;
        }

        public async Task<bool> InsertAllUsers(List<Users> users)
        {
            bool result = false;

            try
            {
                if (users != null && users.Count > 0)
                {
                    string query = UserCxSQLStatements.InsertUserData;
                    
                    users.ForEach(async item =>
                    {
                        DynamicParameters dynamicParameters = new();
                        var passwordHasher = new PasswordHasher<Users>();
                        var passwordHash = passwordHasher.HashPassword(item, item.UserPassword);
                        var seachField = ClearText(item.UserName);

                        dynamicParameters.Add("P_NAME_USER", item.UserName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                        dynamicParameters.Add("P_EMAIL_USER", item.UserEmail, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                        dynamicParameters.Add("P_AGE_USER", item.UserAge, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                        dynamicParameters.Add("P_GENDER_USER", item.UserGender.ToUpper().Trim(), System.Data.DbType.String, System.Data.ParameterDirection.Input);
                        dynamicParameters.Add("P_PASSWORD_USER", passwordHash, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                        dynamicParameters.Add("P_OFFICIAL_NUMBER_USER", FormatCpf(item.UserOfficialNumber), System.Data.DbType.String, System.Data.ParameterDirection.Input);
                        dynamicParameters.Add("P_SEARCH_FIELD", seachField.Replace(" ", ""), System.Data.DbType.String, System.Data.ParameterDirection.Input);
                        dynamicParameters.Add("P_USER_ID", item.UserID, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                        await _dbMySQLSession.ExecuteScalarAsync(query, dynamicParameters);
                    });

                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[InsertAllUsers] - Erro ao inserir na tabela USERS_CX: {ex.Message}");
            }

            return result;
        }

        public async Task<int> GetUserEmailExists(string email)
        {
            int result = 0;

            try
            {
                if (!string.IsNullOrWhiteSpace(email))
                {
                    string query = UserCxSQLStatements.GetUserEmailExists;
                    DynamicParameters dynamicParameters = new();

                    dynamicParameters.Add("P_EMAIL_USER", email.ToUpper().Trim(), System.Data.DbType.String, System.Data.ParameterDirection.Input);

                    result = await _dbMySQLSession.ExecuteScalarAsync<int>(query, dynamicParameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[GetUserEmailExists] - Erro ao verificar se o e-mail existe na tabela USERS_CX: {ex.Message}"); ;
            }

            return result;
        }

        public async Task<int> GetUserOfficialNumberExists(string officialNumber)
        {
            int result = 0;

            try
            {
                if (!string.IsNullOrWhiteSpace(officialNumber))
                {
                    string query = UserCxSQLStatements.GetUserOfficialNumberExists;
                    DynamicParameters dynamicParameters = new();

                    dynamicParameters.Add("P_OFFICIAL_NUMBER_USER", FormatCpf(officialNumber), System.Data.DbType.String, System.Data.ParameterDirection.Input);

                    result = await _dbMySQLSession.ExecuteScalarAsync<int>(query, dynamicParameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[GetUserOfficialNumberExists] - Erro ao verificar se o CPF existe na tabela USERS_CX: {ex.Message}"); ;
            }

            return result;
        }

        private string ClearText(string input)
        {
            string textNormalize = input.Normalize(NormalizationForm.FormD);
            string noAccent = Regex.Replace(textNormalize, @"\p{Mn}+", "");
            string clearInput = Regex.Replace(noAccent, @"[^a-zA-Z0-9\s]", "");

            return clearInput.ToUpper();
        }

        private string FormatCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return string.Empty;

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return cpf; // retorna como está se não tiver 11 dígitos

            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }
    }
}
