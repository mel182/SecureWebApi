using SecureWebAPi.Api.Response.Model;
using SecureWebAPi.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Interfaces
{
    interface IAuthUserService
    {
        IEnumerable<RegisteredUser> GetAllRegisteredUsers();
        Task<RegisteredUser> AddUserAsync(AuthenticatedUser newUser);
        RegisteredUser GetUserById(long id);
        void RemoveUser(long id);

        IEnumerable<RegisteredUser> GetAllRegisteredAdmins();
        RegisteredUser AddAdmin(AuthenticatedUser newAdmin);
        RegisteredUser GetAdminById(long id);
        void RemoveAdmin(long id);

    }
}
