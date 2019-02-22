using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Database.Model
{
    public class AuthenticatedUser
    {
        public long ID { get; set; } = 0;
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public long CreationDate { get; set; } = 0;
        public long Updated { get; set; } = 0;
        public bool Admin { get; set; } = false;
    }
}
