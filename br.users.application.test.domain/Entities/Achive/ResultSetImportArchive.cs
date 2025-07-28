using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Entities.Achive
{
    public class ResultSetImportArchive
    {
        public string? ResultFileContent { get; set; }

        public int CountRows { get; set; }
    }
}
