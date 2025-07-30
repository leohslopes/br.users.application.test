using br.users.application.test.domain.Entities.Dashboard;
using br.users.application.test.domain.Interfaces.Messaging;
using br.users.application.test.domain.Interfaces.Repositories;
using br.users.application.test.domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.application.Services
{
    public class DashboardService : BaseService, IDashboardService
    {
        private readonly ILogger<DashboardService> _logger;
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(ILogger<DashboardService> logger, IDashboardRepository dashboardRepository)
        {
            _logger = logger;
            _dashboardRepository = dashboardRepository;
        }

        public async Task<IEnumerable<ReportUsersDashboard>> GetReportTotalUsers()
        {
            IEnumerable<ReportUsersDashboard> result;

            try
            {
                result = await _dashboardRepository.GetTotalUsersByMonths();
            }
            catch (ApplicationException ex)
            {
                _logger.LogError($"[GetReportTotalUsers] - Erro ao consultar o relatório de total de usuários no banco de dados: {ex.Message}");
                throw ex;
            }

            return result;
        }
    }
}
