using br.users.application.test.domain.Entities.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Interfaces.Repositories
{
    public interface IDashboardRepository
    {
        Task<IEnumerable<ReportUsersDashboard>> GetTotalUsersByMonths();
    }
}
