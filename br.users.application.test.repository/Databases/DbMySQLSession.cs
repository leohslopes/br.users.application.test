using br.users.application.test.domain.Entities;
using br.users.application.test.repository.Databases.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace br.users.application.test.repository.Databases
{
    public class DbMySQLSession : IDbMySQLSession
    {
        private readonly IConfiguration _configuration;
        private readonly string _userConnectionString;
        private readonly AppSettings _appSettings;
        private readonly MySqlConnection _dbConnection;
        public bool Disposed { get; set; }

        public DbMySQLSession(IConfiguration configuration, AppSettings appSettings)
        {
            _configuration = configuration;
            _appSettings = appSettings;
            _userConnectionString = _appSettings.ConnectionStrings.UserCxConnection;
            _dbConnection = new MySqlConnection(_userConnectionString);
        }

        public async Task<dynamic> QueryFirstAsync(string Sql, DynamicParameters parameters = null)
        {
            await OpenConnectionAsync();
            try
            {
                var result = await _dbConnection.QueryFirstAsync(Sql, parameters);
                await CloseConnectionAsync();
                return result;
            }
            catch (Exception)
            {
                await CloseConnectionAsync();
                throw;
            }
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string Sql, DynamicParameters parameters = null)
        {
            await OpenConnectionAsync();
            try
            {
                var result = await _dbConnection.QueryAsync<T>(Sql, parameters);
                await CloseConnectionAsync();
                return result;
            }
            catch (Exception)
            {
                await CloseConnectionAsync();
                throw;
            }
        }
        public async Task<T> ExecuteScalarAsync<T>(string Sql, DynamicParameters parameters = null)
        {
            await OpenConnectionAsync();
            try
            {
                var result = await _dbConnection.ExecuteScalarAsync<T>(Sql, parameters);
                await CloseConnectionAsync();
                return result;
            }
            catch (Exception)
            {
                await CloseConnectionAsync();
                throw;
            }
        }
        public async Task<object> ExecuteScalarAsync(string Sql, DynamicParameters parameters = null)
        {
            await OpenConnectionAsync();
            try
            {
                var result = await _dbConnection.ExecuteScalarAsync(Sql, parameters);
                await CloseConnectionAsync();
                return result;
            }
            catch (Exception)
            {
                await CloseConnectionAsync();
                throw;
            }
        }

        public async Task OpenConnectionAsync()
        {
            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    await _dbConnection.OpenAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CloseConnectionAsync()
        {
            try
            {
                if (_dbConnection.State == ConnectionState.Open)
                    await _dbConnection.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async ValueTask DisposeAsync()
        {
            Disposed = true;
            await _dbConnection.DisposeAsync();
        }
    }
}
