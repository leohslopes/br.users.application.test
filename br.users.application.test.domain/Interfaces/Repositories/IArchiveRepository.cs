using br.users.application.test.domain.Entities.Achive;
using br.users.application.test.domain.Entities.UserCx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Interfaces.Repositories
{
    public interface IArchiveRepository
    {
        Task<bool> InsertAllUsers(List<Users> users);

        Task<int> GetUserEmailExists(string email);

        Task<int> GetUserOfficialNumberExists(string officialNumber);
    }
}
