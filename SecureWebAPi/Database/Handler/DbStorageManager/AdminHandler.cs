using Microsoft.AspNetCore.Mvc;
using SecureWebAPi.Api.Response.Model;
using SecureWebAPi.Database.Handler.DbStorageManage;
using SecureWebAPi.Database.Model;
using SecureWebAPi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Database.Handler.DbStorageManager
{
    public class AdminHandler : StorageHandlerManager
    {

        private static readonly Lazy<AdminHandler> AdminHandlerInstance = new Lazy<AdminHandler>(() => new AdminHandler());
        private IServiceProvider ServiceProvider { get; set; } = null;

        private AdminHandler() { }

        internal static AdminHandler Get
        {
            get
            {
                return AdminHandlerInstance.Value;
            }
        }

        public async Task<ActionResult<IEnumerable<RegisteredUser>>> GetAllAdmin()
        {
            List<RegisteredUser> registeredAdmins = new List<RegisteredUser>();

            if (Context != null)
            {
                var adminUserList = Context.AuthenticatedUsers.Where(registeredUser => registeredUser.Admin == true).ToList();

                foreach(AuthenticatedUser adminUserFound in adminUserList)
                {
                    registeredAdmins.Add(adminUserFound.ToRegisteredUser(RoleType.ADMIN));
                }
            }

            return registeredAdmins;
        }


    }
}
