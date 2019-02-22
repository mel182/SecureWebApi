using SecureWebAPi.Api.Response.Model;
using SecureWebAPi.Database;
using SecureWebAPi.Database.Handler.DbStorageManage;
using SecureWebAPi.Database.Model;
using SecureWebAPi.DateTimeGenerator;
using SecureWebAPi.Security.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Extensions
{
    public static class AppExtension
    {

        public static RegisteredUser ToRegisteredUser(this AuthenticatedUser authenticatedUser, RoleType roleType)
        {
            return new RegisteredUser
            {
                ID = authenticatedUser.ID,
                FirstName = authenticatedUser.FirstName,
                LastName = authenticatedUser.LastName,
                Email = authenticatedUser.Email,
                UserName = authenticatedUser.UserName,
                Role = roleType.ToString(),
                CreationDate = authenticatedUser.CreationDate,
                Updated = authenticatedUser.Updated
            };
        }

        public static bool IsInitialized(this DatabaseRepository context)
        {
            return context != null;
        }

        public static AuthenticatedUser AddTimeStamps(this AuthenticatedUser authenticatedUser)
        {
            authenticatedUser.CreationDate = TimeStampGenerator.CurrentTimeStamp;
            authenticatedUser.Updated = TimeStampGenerator.CurrentTimeStamp;
            
            return authenticatedUser;
        }

        public static AuthenticatedUser EncodePassword(this AuthenticatedUser authenticatedUser)
        {
            var encodedPassword = Base64Encoder.Encode(authenticatedUser.Password);
            authenticatedUser.Password = encodedPassword;

            return authenticatedUser;
        }

        public static AuthenticatedUser AsUser(this AuthenticatedUser authenticatedUser)
        {
            authenticatedUser.Admin = false;

            return authenticatedUser;
        }

        public static AuthenticatedUser AsAdmin(this AuthenticatedUser authenticatedUser)
        {
            authenticatedUser.Admin = true;

            return authenticatedUser;
        }
        //AuthenticatedUser
    }
}
