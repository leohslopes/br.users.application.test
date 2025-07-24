using br.users.application.test.domain.Entities;
using br.users.application.test.domain.Entities.UserCx;
using br.users.application.test.domain.Interfaces.Repositories;
using br.users.application.test.repository.Databases.Interfaces;
using br.users.application.test.repository.Repositories.SQLStatement;
using Dapper;
using Microsoft.AspNetCore.Http;
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
    public class UserCxRepository : IUserCxRepository
    {
        private readonly ILogger<UserCxRepository> _logger;
        private readonly IDbMySQLSession _dbMySQLSession;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        public UserCxRepository(IConfiguration configuration, ILogger<UserCxRepository> logger, IDbMySQLSession dbMySQLSession, AppSettings appSettings)
        {
            _logger = logger;
            _configuration = configuration;
            _dbMySQLSession = dbMySQLSession;
            _appSettings = appSettings;
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            string query = UserCxSQLStatements.GetAllUsers;

            var result = await _dbMySQLSession.QueryAsync<Users>(query);

            return result;
        }

        public async Task InsertUserData(string nameUser, string emailUser, int ageUser, string genderUser, string passwordUser, string officialNumberUser)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(nameUser) && 
                    !string.IsNullOrWhiteSpace(emailUser) && 
                    ageUser != 0 && 
                    !string.IsNullOrWhiteSpace(genderUser) && 
                    !string.IsNullOrWhiteSpace(passwordUser) &&
                    !string.IsNullOrWhiteSpace(officialNumberUser))
                {
                    var user = new Users() { UserID = 0, UserName = nameUser, UserEmail = emailUser, UserAge = ageUser, UserGender = genderUser, UserPassword = passwordUser, UserOfficialNumber = officialNumberUser};
                    var passwordHasher = new PasswordHasher<Users>();
                    var passwordHash = passwordHasher.HashPassword(user, user.UserPassword);
                    var seachField = await ClearText(user.UserName);

                    string query = UserCxSQLStatements.InsertUserData;
                    DynamicParameters dynamicParameters = new();
                    dynamicParameters.Add("P_NAME_USER", user.UserName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    dynamicParameters.Add("P_EMAIL_USER", user.UserEmail, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    dynamicParameters.Add("P_AGE_USER", user.UserAge, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                    dynamicParameters.Add("P_GENDER_USER", user.UserGender.ToUpper().Trim(), System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    dynamicParameters.Add("P_PASSWORD_USER", passwordHash, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    dynamicParameters.Add("P_OFFICIAL_NUMBER_USER", user.UserOfficialNumber, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    dynamicParameters.Add("P_SEARCH_FIELD", seachField.Replace(" ",""), System.Data.DbType.String, System.Data.ParameterDirection.Input);

                    await _dbMySQLSession.ExecuteScalarAsync(query, dynamicParameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[InsertUserData] - Erro ao inserir na tabela USERS_CX: {ex.Message}");
            }
        }

        public async Task UpdateUserData(int userID, string nameUser, string emailUser, int ageUser, string genderUser, string passwordUser, IFormFile? pictureUser, string officialNumberUser)
        {
            try
            {
                if (userID != 0 && 
                    !string.IsNullOrWhiteSpace(nameUser) && 
                    !string.IsNullOrWhiteSpace(emailUser) && 
                    ageUser != 0 && 
                    !string.IsNullOrWhiteSpace(genderUser) &&
                    !string.IsNullOrWhiteSpace(officialNumberUser))
                {
                    var user = new Users() { UserID = userID, UserName = nameUser, UserEmail = emailUser, UserAge = ageUser, UserGender = genderUser, UserPassword = passwordUser, UserOfficialNumber = officialNumberUser };
                    var passwordHasher = new PasswordHasher<Users>();
                    var passwordHash = passwordHasher.HashPassword(user, user.UserPassword);
                    var imgBytes = await GetImageBytes(pictureUser);
                    var seachField = await ClearText(user.UserName);

                    string query = UserCxSQLStatements.UpdateUserData;
                    DynamicParameters dynamicParameters = new();
                    dynamicParameters.Add("P_NAME_USER", user.UserName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    dynamicParameters.Add("P_EMAIL_USER", user.UserEmail, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    dynamicParameters.Add("P_AGE_USER", user.UserAge, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                    dynamicParameters.Add("P_GENDER_USER", user.UserGender.ToUpper().Trim(), System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    dynamicParameters.Add("P_PASSWORD_USER", passwordHash, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    dynamicParameters.Add("P_PICTURE_USER", imgBytes, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
                    dynamicParameters.Add("P_OFFICIAL_NUMBER_USER", user.UserOfficialNumber, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    dynamicParameters.Add("P_SEARCH_FIELD", seachField.Replace(" ", ""), System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    dynamicParameters.Add("P_USER_ID", user.UserID, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                    await _dbMySQLSession.ExecuteScalarAsync(query, dynamicParameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[UpdateUserData] - Erro ao atualizar a tabela USERS_CX: {ex.Message}");
            }
        }

        public async Task DeleteUserData(int userID)
        {
            try
            {
                if (userID != 0)
                {
                    string query = UserCxSQLStatements.DeleteUserData;
                    DynamicParameters dynamicParameters = new();
                    dynamicParameters.Add("P_USER_ID", userID, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                    await _dbMySQLSession.ExecuteScalarAsync(query, dynamicParameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[DeleteUserData] - Erro ao excluir da tabela USERS_CX: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Users>> GetUsersWithFilters(string filterName, string filterEmail, bool? filterImg)
        {
            IEnumerable<Users> result;

            try
            {
                string query = UserCxSQLStatements.GetUsersWithFilters;
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("P_SEARCH_FIELD", !string.IsNullOrEmpty(filterName) ? $"%{filterName.ToUpper().Trim()}%" : DBNull.Value, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                dynamicParameters.Add("P_EMAIL_USER", !string.IsNullOrEmpty(filterEmail) ? filterEmail.ToUpper().Trim() : DBNull.Value, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                dynamicParameters.Add("P_HAS_IMG", filterImg.HasValue? filterImg.Value : DBNull.Value, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);

                result = await _dbMySQLSession.QueryAsync<Users>(query, dynamicParameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"[GetUsersWithFilters] - Erro ao fazer a consulta dos dados na tabela USERS_CX com filtros de nome e/ou e-mail: {ex.Message}");
            }

            return result;
        }

        private async Task<byte[]> GetImageBytes(IFormFile? pictureUser)
        {
            byte[]? imgBytes = null;

            if (pictureUser is not null)
            {
                using var ms = new MemoryStream();
                await pictureUser.CopyToAsync(ms);
                imgBytes = ms.ToArray();
            }

            return imgBytes;
        }

        private async Task<string> ClearText(string input)
        {
            string textNormalize = input.Normalize(NormalizationForm.FormD);
            string noAccent = Regex.Replace(textNormalize, @"\p{Mn}+", "");
            string clearInput = Regex.Replace(noAccent, @"[^a-zA-Z0-9\s]", "");

            return clearInput.ToUpper();
        }
    }
}
