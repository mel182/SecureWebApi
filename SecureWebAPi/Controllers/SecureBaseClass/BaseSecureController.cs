using Microsoft.AspNetCore.Mvc;
using SecureWebAPi.Database.Handler;
using SecureWebAPi.Database.Handler.DbStorageManager;
using SecureWebAPi.Database.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Controllers.SecureBaseClass
{
    public abstract class BaseSecureController : ControllerBase
    {

        //public AdminHandler AdminHandler = DbHandler.Get.AdminHandler;
        public AdminService AdminService = new AdminService();

        // Get JWT claims

        // IsAuthorized

        // Isadmin
    }
}
