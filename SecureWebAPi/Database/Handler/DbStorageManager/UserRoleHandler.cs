using SecureWebAPi.Database.Handler.DbStorageManage;
using SecureWebAPi.Database.Model;
using System;
using System.Linq;
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

        public async Task InitializeAsync(DatabaseContext databaseContext)
        {
            SetContext(databaseContext);
            await RoleHandler.Get.StoreRolesAsync();
        }
        
        public async Task<bool> StoreRoleAsync(int userID, RoleType roleType)
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

                return true;
            }
            
            return false;
        }
    }

    //--------------------------
    //public class RoleHandler : StorageHandlerManager
    //{
    //    private static readonly Lazy<RoleHandler> RolehandlerInstance = new Lazy<RoleHandler>(() => new RoleHandler());
    //    private IServiceProvider ServiceProvider { get; set; } = null;

    //    private RoleHandler() { }

    //    protected static RoleHandler Get
    //    {
    //        get
    //        {
    //            return RolehandlerInstance.Value;
    //        }
    //    }

    //    protected async Task StoreRolesAsync()
    //    {
    //        if (Context != null)
    //        {
    //            foreach (var item in Enum.GetNames(typeof(RoleType)))
    //            {
    //                Role role = new Role
    //                {
    //                    RoleName = item.ToString()
    //                };

    //                this.Context.Roles.Add(role);
    //            }

    //            await this.Context.SaveChangesAsync();
    //        }
    //    }

    //    protected int GetRoleID(RoleType roleType)
    //    {
    //        if (Context != null)
    //        {
    //            var roleEntity = Context.Roles.Where(roleFound => roleFound.RoleName.Equals(roleType.ToString())).First();
    //            if (roleEntity != null)
    //            {
    //                return roleEntity.ID;
    //            }
    //        }

    //        return -1;
    //    }

    //}
    // ----------------

    //public enum RoleType
    //{
    //    USER,
    //    ADMIN,
    //    ROOT
    //}
}
