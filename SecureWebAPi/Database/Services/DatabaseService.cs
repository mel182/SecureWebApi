using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SecureWebAPi.Api.Response.Model;
using SecureWebAPi.Database.Handler.DbStorageManage;
using SecureWebAPi.Database.Handler.DbStorageManager;
using SecureWebAPi.Database.Model;
using SecureWebAPi.Database.Services.BaseClass;
using SecureWebAPi.Extensions;
using SecureWebAPi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Database.Services
{
    public class DatabaseService : DatabaseHandlerManager, IAuthUserService
    {
        private static readonly Lazy<DatabaseService> DbServiceInstance = new Lazy<DatabaseService>(() => new DatabaseService());

        private DatabaseService() { }

        public static DatabaseService Get
        {
            get
            {
                return DbServiceInstance.Value;
            }
        }

        //public async Task<RegisteredUser> AddUserAsync(AuthenticatedUser authenticatedUser)
        //{
        //    if(Context.IsInitialized())
        //    {
        //        try
        //        {
        //            var refineUserInput = authenticatedUser
        //                .EncodePassword()
        //                .AddTimeStamps()
        //                .AsUser();

        //            Context.AuthenticatedUsers.Add(refineUserInput);
        //            await Context.SaveChangesAsync();
        //            var newUser = Context.AuthenticatedUsers.Where(authUser => authUser.UserName.Equals(authenticatedUser.UserName)).First();
        //            if (newUser != null)
        //            {
        //                var newUserRole = await UserRoleHandler.Get.StoreRoleAsync(newUser.ID, roleType: RoleType.USER);

        //                if (newUserRole != null)
        //                {
        //                    return new RegisteredUser
        //                    {
        //                        ID = newUser.ID,
        //                        FirstName = newUser.FirstName,
        //                        LastName = newUser.LastName,
        //                        Email = newUser.Email,
        //                        UserName = newUser.UserName,
        //                        Role = RoleType.USER.ToString(),
        //                        CreationDate = newUser.CreationDate,
        //                        Updated = newUser.Updated
        //                    };
        //                }
        //            }
        //        }
        //        catch (DbUpdateException) { }
        //        catch (Exception) { }
        //    }

        //    return null;
        //}

        public async Task<RegisteredUser> AddAdminAsync(AuthenticatedUser authenticatedUser)
        {
            if (Context.IsInitialized())
            {
                try
                {
                    var user = authenticatedUser.AddTimeStamps();

                    Context.AuthenticatedUsers.Add(user);
                    await Context.SaveChangesAsync();
                    var newUser = Context.AuthenticatedUsers.Where(authUser => authUser.UserName.Equals(authenticatedUser.UserName)).First();
                    if (newUser != null)
                    {
                        var newUserRole = await UserRoleHandler.Get.StoreRoleAsync(newUser.ID, roleType: RoleType.ADMIN);

                        if (newUserRole != null)
                        {
                            return new RegisteredUser
                            {
                                ID = newUser.ID,
                                FirstName = newUser.FirstName,
                                LastName = newUser.LastName,
                                Email = newUser.Email,
                                UserName = newUser.UserName,
                                Role = RoleType.ADMIN.ToString(),
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

        

        private async Task<bool> SucceedSettingContextAsync()
        {
            try
            {
                this.Context = this.ServiceProvider.GetRequiredService<DatabaseRepository>();

                var RolesInitialized = Role_Handler
                    .SetRepositoryContext(this.Context)
                    .Initialize()
                    .Result;

                return RolesInitialized;
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

        public IEnumerable<RegisteredUser> GetAllRegisteredUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<RegisteredUser> AddUserAsync(AuthenticatedUser newAuthUser)
        {
            if (Context.IsInitialized())
            {
                try
                {
                    var refineUserInput = newAuthUser
                        .EncodePassword()
                        .AddTimeStamps()
                        .AsUser();

                    Context.AuthenticatedUsers.Add(refineUserInput);
                    await Context.SaveChangesAsync();
                    var newUser = Context.AuthenticatedUsers.Where(authUser => authUser.UserName.Equals(authenticatedUser.UserName)).First();
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

        public RegisteredUser GetUserById(long id)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegisteredUser> GetAllRegisteredAdmins()
        {
            throw new NotImplementedException();
        }

        public RegisteredUser AddAdmin(AuthenticatedUser newItem)
        {
            throw new NotImplementedException();
        }

        public RegisteredUser GetAdminById(long id)
        {
            throw new NotImplementedException();
        }

        public void RemoveAdmin(long id)
        {
            throw new NotImplementedException();
        }

        


        // --------------------------


    }
}
