using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GiveFoodData;
using GiveFoodDataModels;
using GiveFoodServices.Roles;
using GiveFoodServices.Documents;
using Amazon.S3;
using Amazon;
using GiveFoodServices.Admin;
using GiveFood.DAL.Documents;
using GiveFoodServices.Users;
using GiveFoodServices.Users.Models;
using Microsoft.AspNetCore.Http;

namespace GiveFoodWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GiveFoodDbContext>(options =>
           options.UseSqlServer(Configuration["GiveFoodDatabase"]));

             
            services.AddIdentity<User, ApplicationRole>()
                    .AddEntityFrameworkStores<GiveFoodDbContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromMinutes(120);
                options.LoginPath = "/Account/LogIn"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/LogOut"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });



            services.AddMvc();//.AddWebApiConventions();

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("example.com");
                options.ExcludedHosts.Add("www.example.com");
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });

            services.AddScoped<IAmazonS3>(s => new AmazonS3Client(
                Configuration["AmazonS3:UserName"],
                Configuration["AmazonS3:UserSecret"],
                RegionEndpoint.EUCentral1));

            //app services and repos
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAmazonService, AmazonService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddSingleton<IEmailService, EmailService>();
          
            services.Configure<AuthMessageSenderOptions>(Configuration);
              services.AddScoped<IAuthService, AuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
                //ConfigurationBuilder.AddUserSecrets<Startup>();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

           // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<GiveFoodDbContext>();
                // context.Database.EnsureCreated();
            }
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}"
                    );

                routes.MapRoute(
                    name: "defaultWithId",
                    template: "api/{controller}/{action}/{id?}"
                    );
            });
        }
    }
}
