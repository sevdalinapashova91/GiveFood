using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GiveFoodData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GiveFoodWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = BuildWebHost(args);
            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var context = services.GetRequiredService<GiveFoodDbContext>();
            //    //context.Database.Migrate();
            //    context.Database.EnsureCreated();
            //    try
            //    {
            //        SeedData.Initialize(services, "Admin098@").Wait();
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = services.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex.Message, "An error occurred seeding the DB.");
            //    }
            //}
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
