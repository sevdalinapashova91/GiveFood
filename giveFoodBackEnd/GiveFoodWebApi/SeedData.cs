using GiveFoodData;
using GiveFoodDataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GiveFoodWebApi
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string password)
        {
            using (var context = new GiveFoodDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<GiveFoodDbContext>>()))
            {
                var adminID = await EnsureUser(serviceProvider, password, "admin@admin.com", "Admin", UserType.Admin);

                await EnsureRole(serviceProvider, adminID, UserType.Admin.ToString());

                await EnsureUser(serviceProvider, password, "giver@giver.com", "Giver Corp", UserType.Giver);

                await EnsureUser(serviceProvider, password, "taker@taker.com", "Taker Corp", UserType.Taker);
            }
        }

        private static async Task<string> EnsureUser(
            IServiceProvider serviceProvider,
            string password, 
            string email,
            string name,
            UserType typeId)
        {
            var userManager = serviceProvider.GetService<UserManager<User>>();

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new User { UserName = email, Email = email, Type = typeId };
                var result = await userManager.CreateAsync(user, password);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(
            IServiceProvider serviceProvider,
            string uid,
            string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<UserRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new UserRole()
                {
                    Name = role,
                    Description = role,
                    CreatedDate = DateTime.UtcNow,
                    IPAddress = GetLocalIPAddress ()
                });
            }

            var userManager = serviceProvider.GetService<UserManager<User>>();

            var user = await userManager.FindByIdAsync(uid);

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
