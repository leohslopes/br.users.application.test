using br.users.application.test.domain.Entities.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<IEnumerable<ReportUsersDashboard>> GetReportTotalUsers();

        Task<IEnumerable<ReportUserGender>> GetReportTotalUserGenders();

        Task<IEnumerable<ReportUserAllAges>> GetReportUserAllAges();
    }
}
