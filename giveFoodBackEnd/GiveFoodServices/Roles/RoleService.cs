using GiveFoodDataModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace GiveFoodServices.Roles
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public RoleService(UserManager<User> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task AssignRoleToUser(string userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await userManager.AddToRoleAsync(user, roleName);
            }
        }

        public async Task CreateRolesandAdminUser()
        {
            bool exists = await roleManager.RoleExistsAsync("Admin");
            if (!exists)
            {
                var role = new ApplicationRole();
                role.Name = Role.Admin.ToString();
                role.CreatedDate = DateTime.Now;
                await roleManager.CreateAsync(role);
                var user = new User
                {
                    UserName = "Admin",
                    Email = "admin@givefood.com"
                };

                string userPWD = "Admin098@";
                IdentityResult chkUser = await userManager.CreateAsync(user, userPWD);
                if (chkUser.Succeeded)
                {
                    var result1 = await userManager.AddToRoleAsync(user, Role.Admin.ToString());
                }
            }

            exists = await roleManager.RoleExistsAsync(Role.Giver.ToString());
            if (!exists)
            {
                var role = new ApplicationRole
                {
                    Name = Role.Giver.ToString(),
                    CreatedDate = DateTime.Now,
                    Description = "Profitable organization in food market"
                };
                await roleManager.CreateAsync(role);

            }

            exists = await roleManager.RoleExistsAsync(Role.Taker.ToString());
            if (!exists)
            {
                var role = new ApplicationRole
                {
                    Name = Role.Taker.ToString(),
                    CreatedDate = DateTime.Now,
                    Description = "Non profitable organization"
                };
                await roleManager.CreateAsync(role);
            }
        }

    }
}
