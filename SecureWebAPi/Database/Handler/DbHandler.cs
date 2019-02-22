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

        private DatabaseRepository Context { get; set; } = null;
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
                this.Context = this.ServiceProvider.GetRequiredService<DatabaseRepository>();

                var roleHandler = RoleHandler.Get;
                roleHandler.SetRepositoryContext(this.Context);
                AdminHandler.Get.SetRepositoryContext(this.Context);
                UserHandler.Get.SetRepositoryContext(this.Context);
                UserRoleHandler.Get.SetRepositoryContext(this.Context);
                await roleHandler.Initialize();

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

        public AdminHandler AdminHandler
        {
            get
            {
                return AdminHandler.Get;
            }
        }

    }
}
