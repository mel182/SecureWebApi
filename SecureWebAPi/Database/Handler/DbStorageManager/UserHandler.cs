using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureWebAPi.Api.Response.Model;
using SecureWebAPi.Database.Handler.DbStorageManage;
using SecureWebAPi.Database.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Database.Handler.DbStorageManager
{
    public class UserHandler : StorageHandlerManager
    {
        private static readonly Lazy<UserHandler> RolehandlerInstance = new Lazy<UserHandler>(() => new UserHandler());

        private UserHandler() { }

        public static UserHandler Get
        {
            get
            {
                return RolehandlerInstance.Value;
            }
        }

        public async Task<ActionResult<RegisteredUser>> AddNewAuthenticatedUser(AuthenticatedUser authenticatedUser)
        {
            if(Context != null)
            {
                try
                {
                    authenticatedUser.CreationDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    authenticatedUser.Updated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    
                    //var refinedObject = authenticatedUser;
                    //refinedObject.CreationDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    //refinedObject.Updated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                    Context.AuthenticatedUsers.Add(authenticatedUser);
                    await Context.SaveChangesAsync();
                    var newUser = Context.AuthenticatedUsers.Where(user => user.UserName.Equals(authenticatedUser.UserName)).First();
                    if (newUser != null)
                    {
                        var newUserRole = await UserRoleHandler.Get.StoreRoleAsync(newUser.ID, roleType: RoleType.USER);

                        if (newUserRole != null)
                        {
                            return new RegisteredUser
                            {
                                ID = newUser.ID,
                                FirstName = newUser.FirstName,
                                LastName = newUser.LastName,
                                Email = newUser.Email,
                                UserName = newUser.UserName,
                                Role = RoleType.USER.ToString(),
                                CreationDate = newUser.CreationDate,
                                Updated = newUser.Updated
                            };
                        }
                    }
                }
                catch (DbUpdateException) { } 
                catch (Exception) { }
            }
            
            return null;
        }
    }
}
