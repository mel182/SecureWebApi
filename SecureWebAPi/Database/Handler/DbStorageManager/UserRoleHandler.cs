using SecureWebAPi.Api.Response.Model;
using SecureWebAPi.Database.Handler.DbStorageManage;
using SecureWebAPi.Database.Model;
using System;
using System.Threading.Tasks;

namespace SecureWebAPi.Database.Handler.DbStorageManager
{
    public class UserRoleHandler : StorageHandlerManager
    {
        private static readonly Lazy<UserRoleHandler> RolehandlerInstance = new Lazy<UserRoleHandler>(() => new UserRoleHandler());
        
        private UserRoleHandler() { }

        public static UserRoleHandler Get
        {
            get
            {
                return RolehandlerInstance.Value;
            }
        }

        public async Task InitializeAsync(DatabaseRepository databaseContext)
        {
            SetRepositoryContext(databaseContext);
            await RoleHandler.Get.Initialize();
        }
        
        public async Task<UserRole> StoreRoleAsync(long userID, RoleType roleType)
        {
            int RoleID = RoleHandler.Get.GetRoleID(roleType);
            if (RoleID != -1 && Context != null)
            {
                UserRole userRole = new UserRole
                {
                    UserID = userID.ToString(),
                    RoleID = RoleID.ToString()
                };

                Context.UserRoles.Add(userRole);
                await Context.SaveChangesAsync();

                return userRole;
            }
            
            return null;
        }

        //public async Task<bool> Store(int userID, RoleType roleType)
        //{
        //    int RoleID = RoleHandler.Get.GetRoleID(roleType);
        //    if (RoleID != -1 && Context != null)
        //    {
        //        UserRole userRole = new UserRole
        //        {
        //            UserID = userID.ToString(),
        //            RoleID = RoleID.ToString()
        //        };

        //        Context.UserRoles.Add(userRole);
        //        await Context.SaveChangesAsync();

        //        return true;

        //    }
        //}
    }
}
