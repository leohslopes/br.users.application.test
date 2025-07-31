using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Entities.Dashboard
{
    public class ReportUserPicture
    {
        public required string ResultPicture { get; set; }

        public int CountPictures { get; set; }
    }
}
