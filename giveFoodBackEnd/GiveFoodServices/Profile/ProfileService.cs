using GiveFood.DAL.Documents;
using GiveFoodDataModels;
using GiveFoodServices.Users.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GiveFoodServices.Users
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> userManager;
        private readonly IDocumentRepository documentRepository;

        public ProfileService(UserManager<User> userManager,
            IDocumentRepository documentRepository)
        {
            this.userManager = userManager;
            this.documentRepository = documentRepository;
        }

        public async Task<ProfileDto> GetAsync(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var document = await documentRepository.GetByUser(user.Id);
            return new ProfileDto()
            {
                Name = user.Name,
                Email = user.Email,
                Description = user.Description,
                DocumentName = document?.Name ?? string.Empty,
                Type = user.Type
            };
        }

        public async Task UpdateAsync(string name, string id, string description)
        {
            var user = await this.userManager.FindByIdAsync(id);

            user.Name = name;
            user.Description = description;

            await userManager.UpdateAsync(user);
        }
    }
}
