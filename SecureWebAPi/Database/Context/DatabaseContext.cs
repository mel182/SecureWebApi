using Microsoft.EntityFrameworkCore;
using SecureWebAPi.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Database
{
    public class DatabaseContext : DbContext
    {
        public static string ConnectionString { get; set; } = "";
        public DbSet<AuthenticatedUser> AuthenticatedUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
