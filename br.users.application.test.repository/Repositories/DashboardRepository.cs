using br.users.application.test.domain.Entities;
using br.users.application.test.domain.Entities.Dashboard;
using br.users.application.test.domain.Interfaces.Repositories;
using br.users.application.test.repository.Databases.Interfaces;
using br.users.application.test.repository.Repositories.SQLStatement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.repository.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ILogger<DashboardRepository> _logger;
        private readonly IDbMySQLSession _dbMySQLSession;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        public DashboardRepository(IConfiguration configuration, ILogger<DashboardRepository> logger, IDbMySQLSession dbMySQLSession, AppSettings appSettings)
        {
            _logger = logger;
            _configuration = configuration;
            _dbMySQLSession = dbMySQLSession;
            _appSettings = appSettings;
        }

        public async Task<IEnumerable<ReportUsersDashboard>> GetTotalUsersByMonths()
        {
            string query = UserCxSQLStatements.GetTotalUsersByMonths;

            var result = await _dbMySQLSession.QueryAsync<ReportUsersDashboard>(query);

            return result;
        }

        public async Task<IEnumerable<ReportUserGender>> GetReportUserByGender()
        {
            string query = UserCxSQLStatements.GetReportUserByGender;

            var result = await _dbMySQLSession.QueryAsync<ReportUserGender>(query);

            return result;
        }

        public async Task<IEnumerable<ReportUserAllAges>> GetResultUserAges()
        {
            string query = UserCxSQLStatements.GetResultUserAges;

            var result = await _dbMySQLSession.QueryAsync<ReportUserAllAges>(query);

            return result;
        }
    }
}
