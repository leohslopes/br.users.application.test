using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Entities.Dashboard
{
    public class ReportUsersDashboard
    {
        public required string Years { get; set; }

        public required string MonthName { get; set; }

        public int CountUsers { get; set; }
    }
}
