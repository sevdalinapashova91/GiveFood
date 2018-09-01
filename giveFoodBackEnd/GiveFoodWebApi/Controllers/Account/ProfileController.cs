using GiveFoodServices.Users;
using GiveFoodServices.Users.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Controllers.Account
{
    public class ProfileController : Controller
    {
        public readonly ProfileService profileService;

        public ProfileController(ProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet]
        public Task<ProfileDto> Get(string email) => profileService.GetAsync(email);

        [HttpPost]
        public Task UpdateUserProfile([FromBody]ProfileInputModel inputModel) => profileService.UpdateNameAsync(inputModel.Name, string.Empty);
    }
}