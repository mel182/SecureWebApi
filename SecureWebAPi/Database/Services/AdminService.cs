using Microsoft.AspNetCore.Mvc;
using SecureWebAPi.Api.Response.Model;
using SecureWebAPi.Database.Handler.DbStorageManager;
using SecureWebAPi.Database.Services.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Database.Services
{
    public class AdminService : DatabaseHandlerManager
    {
        public async Task<ActionResult<IEnumerable<RegisteredUser>>> GetAll()
        {
            return await AdminHandler.GetAllAdmin();
        }
    }
}
