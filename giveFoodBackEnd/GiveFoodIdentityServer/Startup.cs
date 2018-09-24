using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Models;
using IdentityServer4.Test;
using GiveFoodData;
using Microsoft.EntityFrameworkCore;
using GiveFoodDataModels;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using IdentityServer4;
using Microsoft.IdentityModel.Tokens;

namespace GiveFoodIdentityServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GiveFoodDbContext>(options =>
                 options.UseSqlServer("Data Source=.\\SQLExpress;Initial Catalog=GiveFood;Trusted_Connection=True;MultipleActiveResultSets=True;"));

            services.AddIdentity<User, UserRole>()
                   .AddEntityFrameworkStores<GiveFoodDbContext>()
                   .AddDefaultTokenProviders();

            services.AddMvc();

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
              .AddInMemoryIdentityResources(Config.GetIdentityResources())
              .AddInMemoryApiResources(Config.GetApiResources())
              .AddInMemoryClients(Config.GetClients())
              .AddAspNetIdentity<User>()
              .AddProfileService<IdentityWithAdditionalClaimsProfileService>();

           

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}
