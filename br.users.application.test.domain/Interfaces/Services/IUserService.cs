using br.users.application.test.domain.Entities.UserCx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetItemsUserList();

        Task<bool> SaveNewResgisterUserData(string nameUser, string emailUser, int ageUser, string genderUser, string passwordUser);

        Task<Users> GetUserByEmail(string email);
    }
}
