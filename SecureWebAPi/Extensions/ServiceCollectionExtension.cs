using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecureWebAPi.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureMySqlContext(this IServiceCollection serviceCollection, IConfiguration Configuration, string connectionString)
        {
            DatabaseRepository.ConnectionString = Configuration.GetConnectionString(connectionString);

            serviceCollection
                .AddDbContext<DatabaseRepository>(options =>
                options.UseSqlServer(DatabaseRepository.ConnectionString));
        }

        public static void CompatibilityVersion(this IServiceCollection serviceCollection, CompatibilityVersion version)
        {
            serviceCollection
                 .AddMvc()
                 .SetCompatibilityVersion(version);
        }
    }
}
