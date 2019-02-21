using SecureWebAPi.Api.Response.Model;
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

            if(Context != null)
            {
                return await Context.AuthenticatedUsers.ToList();
            }
            
        }


    }
}
