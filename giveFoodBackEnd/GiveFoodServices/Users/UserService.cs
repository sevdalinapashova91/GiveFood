using GiveFoodDataModels;
using GiveFoodServices.Users.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace GiveFoodServices.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public Task<IEnumerable<UserInRoleDto>> GetGiversAsync() => GetUserByTypeAsync(UserType.Giver);

        public Task<IEnumerable<UserInRoleDto>> GetTakersAsync() => GetUserByTypeAsync(UserType.Taker);

        private async Task<IEnumerable<UserInRoleDto>> GetUserByTypeAsync(UserType userType)
        {
            var users = await
                 userManager
                 .GetUsersInRoleAsync(userType.ToString());

            return users.Select(x => new UserInRoleDto
            {
                Name = x.Name,
                Email = x.Email,
                Description = x.Description
            });
        }
    }
}
