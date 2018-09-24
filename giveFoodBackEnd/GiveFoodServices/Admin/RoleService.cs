using GiveFoodDataModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveFoodServices.Roles
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<UserRole> roleManager;

        public RoleService(UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task AssignRoleToUser(string email, string roleName)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await userManager.AddToRoleAsync(user, roleName);
                await userManager.AddClaimAsync(user, new Claim("role", roleName));
                user.Status = UserStatus.Approved;
                await userManager.UpdateAsync(user);

            }
        }

        public async Task<bool> IsRoleExist(string roleName)
        {
            return await roleManager.RoleExistsAsync(roleName);
        }
    }
}
