using br.users.application.test.domain.Entities.UserCx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Interfaces.Repositories
{
    public interface IUserCxRepository
    {
        Task<IEnumerable<Users>> GetAllUsers();

        Task InsertUserData(string nameUser, string emailUser, int ageUser, string genderUser, string passwordUser);

        Task UpdateUserData(int userID, string nameUser, string emailUser, int ageUser, string genderUser, string passwordUser);

        Task DeleteUserData(int userID);
    }
}
