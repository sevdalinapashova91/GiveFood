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
using Microsoft.AspNetCore.Http;
using GiveFood.DAL.Notifications;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using GiveFoodInfrastructure.BackgroudTask;
using GiveFoodInfrastructure.Tasks;
using GiveFoodInfrastructure;
using GiveFoodWebApi.Controllers.Notifications.Authorization;

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

            services.AddIdentity<User, UserRole>()
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
                options.SignIn.RequireConfirmedEmail = false;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddIdentityServerAuthentication(options =>
           {
               options.Authority = "http://localhost:5000";
               options.RequireHttpsMetadata = false;

               options.ApiName = "givefoodapi";
           });

            services.AddMvcCore()
             .AddAuthorization()
             .AddJsonFormatters();

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                       .AllowCredentials(); 
                });
            });

           services.AddSignalR();
           services.AddHangfire(x => x.UseSqlServerStorage(Configuration["GiveFoodDatabase"]));

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
                options.HttpsPort = 32794;
            });

            services.AddScoped<IAmazonS3>(s => new AmazonS3Client(
                Configuration["AmazonS3:UserName"],
                Configuration["AmazonS3:UserSecret"],
                RegionEndpoint.EUCentral1));


            RepositoryRegistration(services);

            ServicesRegistration(services);

            RegisterAuthService(services);
        }

        private void RepositoryRegistration(IServiceCollection services)
        {
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
        }
        private void RegisterAuthService(IServiceCollection services)
        {
            services.AddScoped<INotificationAuthorizationService, NotificationAuthorizationService>();
        }
        private void ServicesRegistration(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IApprovalService, ApprovalService>();
            services.AddSingleton<IRequestApprovalMessageFactory, RequestApprovalMessageFactory>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAmazonService, AmazonService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IBackgroundTask, BackgroundTask>();
            services.AddScoped<ISendNotificationTask, SendNotificationTask>();
            services.AddScoped<INotificationHub, NotificationHub>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseCors("default");

            app.UseAuthentication();

            app.UseHangfireServer();

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub> ("/notify");
            });

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
