using SecureWebAPi.Database.Handler.DbStorageManager;
using SecureWebAPi.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Database.Handler.DbStorageManage
{
    public class RoleHandler : StorageHandlerManager
    {
        private static readonly Lazy<RoleHandler> RolehandlerInstance = new Lazy<RoleHandler>(() => new RoleHandler());
        private IServiceProvider ServiceProvider { get; set; } = null;
        private readonly List<Role> ROLES = new List<Role>()
        {
            new Role{ RoleID = 1, RoleName = RoleType.ROOT.ToString()},
            new Role{ RoleID = 2, RoleName = RoleType.ADMIN.ToString()},
            new Role{ RoleID = 3, RoleName = RoleType.USER.ToString()}
        }; 

        private RoleHandler() { }

        internal static RoleHandler Get
        {
            get
            {
                return RolehandlerInstance.Value;
            }
        }

        internal async Task StoreRolesAsync()
        {
            if (Context != null)
            {
                foreach (Role role in ROLES)
                {
                    if(!RoleExists(role.RoleName))
                        this.Context.Roles.Add(role);
                }

                await this.Context.SaveChangesAsync();
            }
        }

        internal int GetRoleID(RoleType roleType)
        {
            if (Context != null)
            {
                var roleEntity = Context.Roles.Where(roleFound => roleFound.RoleName.Equals(roleType.ToString())).First();
                if (roleEntity != null)
                {
                    return roleEntity.RoleID;
                }
            }

            return -1;
        }
        
        private bool RoleExists(string roleName)
        {
           return Context.Roles.Any(roleEntity => roleEntity.RoleName.Equals(roleName)); 
        }
    }

    public enum RoleType
    {
        USER,
        ADMIN,
        ROOT
    }
}
