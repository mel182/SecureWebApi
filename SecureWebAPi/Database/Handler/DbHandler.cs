using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SecureWebAPi.Database.Handler.DbStorageManage;
using SecureWebAPi.Database.Handler.DbStorageManager;
using SecureWebAPi.Database.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecureWebAPi.Database.Handler
{
    public sealed class DbHandler
    {
        private static readonly Lazy<DbHandler> handler = new Lazy<DbHandler>(() => new DbHandler());

        private DatabaseContext Context { get; set; } = null;
        private IServiceProvider ServiceProvider { get; set; } = null;

        private DbHandler() { }

        public static DbHandler Get
        {
            get
            {
                return handler.Value;
            }
        }

        public DbHandler SetServiceProvider(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
            return this;
        }

        private async Task<bool> SucceedSettingContextAsync()
        {
            try
            {
                this.Context = this.ServiceProvider.GetRequiredService<DatabaseContext>();

                var roleHandler = RoleHandler.Get;
                roleHandler.SetContext(this.Context);
                AdminHandler.Get.SetContext(this.Context);
                UserHandler.Get.SetContext(this.Context);
                UserRoleHandler.Get.SetContext(this.Context);
                await roleHandler.StoreRolesAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public bool Initialize()
        {
            return SucceedSettingContextAsync().Result; 
        }
        
        public async Task<ActionResult<IEnumerable<Role>>> GetUserRoles()
        {
            return await this.Context.Roles.ToListAsync();
        }
    }
}
