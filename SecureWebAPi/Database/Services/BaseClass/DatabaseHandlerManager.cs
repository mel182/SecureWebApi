using Microsoft.Extensions.DependencyInjection;
using SecureWebAPi.Database.Handler.DbStorageManage;
using SecureWebAPi.Database.Handler.DbStorageManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Database.Services.BaseClass
{
    public abstract class DatabaseHandlerManager
    {
        protected IServiceProvider ServiceProvider;
        protected AdminHandler AdminHandler = AdminHandler.Get;
        protected RoleHandler Role_Handler = RoleHandler.Get;
        //Verify all incoming data

        protected DatabaseRepository Context { get; set; }


        //public void SetRepositoryContext(DatabaseRepository databaseContext)
        //{
        //    this.Context = databaseContext;
        //}

        public DatabaseHandlerManager SetServiceProvider(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
            return this;
        }

        private async Task<bool> SucceedSettingContextAsync()
        {
            try
            {
                this.Context = this.ServiceProvider.GetRequiredService<DatabaseRepository>();

                var RoleHandlerInitialized = 
                    Role_Handler
                    .SetRepositoryContext(this.Context)
                    .Initialize()
                    .Result;

                UserRoleHandler.Get.SetRepositoryContext(this.Context);
                return RoleHandlerInitialized;

                //return true;
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



    }
}
