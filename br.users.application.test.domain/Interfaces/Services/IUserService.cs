using br.users.application.test.domain.Entities.UserCx;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetItemsUserList(string filterName, string filterEmail, bool? filterImg);

        Task<bool> SaveNewResgisterUserData(string nameUser, string emailUser, int ageUser, string genderUser, string passwordUser, string officialNumberUser);

        Task<Users> GetUserByEmail(string email);

        Task<bool> UpdateUserRowData(int userID, string nameUser, string emailUser, int ageUser, string genderUser, string passwordUser, IFormFile? pictureUser, string officialNumberUser);

        Task<bool> DeleteUserRowData(int userID);
    }
}
