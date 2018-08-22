using GiveFoodDataModels;
using GiveFoodServices.Users.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GiveFoodServices.Users
{
    public class ProfileService
    {
        private readonly UserManager<User> userManager;

        public ProfileService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ProfileDto> Get(string email)
        {
            var user = await this.userManager.FindByEmailAsync(email);
            return new ProfileDto()
            {
                Name = user.Name,
                Email = user.Email,
                DocumentName = user.Document?.Name?? string.Empty,
                IsApproved = user.Document?.IsApproved ?? false,
            };
        }

        public async Task UpdateName(string name, string email)
        {
            var user = await this.userManager.FindByEmailAsync(email);

            user.Name = name;

            await userManager.UpdateAsync(user);
        }
    }
}
