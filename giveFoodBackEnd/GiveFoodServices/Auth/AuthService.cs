using GiveFoodDataModels;
using GiveFoodServices.Users.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveFoodServices.Users
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;

        public AuthService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task RegisterAsync(UserDto userDto)
        {
            var user = new User
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                Name = userDto.Name,
                Type = userDto.Type
            };

            var result = await userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                await userManager.AddClaimsAsync(user, new Claim[]{
                        new Claim("name", userDto.Name),
                        new Claim("email", user.Email)});
            }
        }
    }
}
