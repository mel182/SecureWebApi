using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SecureWebAPi.Database.Handler;
using SecureWebAPi.Database.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPi.Extensions
{
    public static class AppBuilderExtension
    {
        public static void ConfigureCors(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseCors(options =>
            options.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
        }


        public static void ConfigureDbHandler(this IApplicationBuilder applicationBuilder)
        {
            //DbHandler.Get
            //    .SetServiceProvider(applicationBuilder
            //    .ApplicationServices
            //    .GetRequiredService<IServiceScopeFactory>()
            //    .CreateScope()
            //    .ServiceProvider)
            //    .Initialize();


            DatabaseService.Get.SetServiceProvider(applicationBuilder
                .ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope()
                .ServiceProvider)
                .Initialize();

        }
    }
}
