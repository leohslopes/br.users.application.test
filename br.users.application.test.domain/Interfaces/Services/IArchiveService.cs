using br.users.application.test.domain.Entities.Achive;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Interfaces.Services
{
    public interface IArchiveService
    {
        Task<ResultSetImportArchive> ImportMassiveUsersData(IFormFile file);

        Task<string> ExportReportLogUsersData();

        Task<bool> DeleteReportFileServer();

    }
}
