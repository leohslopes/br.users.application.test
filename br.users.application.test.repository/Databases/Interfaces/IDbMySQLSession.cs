using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.repository.Databases.Interfaces
{
    public  interface IDbMySQLSession : IAsyncDisposable
    {
        Task OpenConnectionAsync();
        Task<dynamic> QueryFirstAsync(string Sql, DynamicParameters parameters = null);
        Task<IEnumerable<T>> QueryAsync<T>(string Sql, DynamicParameters parameters = null);
        Task<T> ExecuteScalarAsync<T>(string Sql, DynamicParameters parameters = null);
        Task<object> ExecuteScalarAsync(string Sql, DynamicParameters parameters = null);
    }
}
