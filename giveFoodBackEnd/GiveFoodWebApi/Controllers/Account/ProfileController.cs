using GiveFoodServices.Users;
using GiveFoodServices.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Controllers.Account
{
    [Authorize]
    public class ProfileController : ApplicationController
    {
        public readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet]
        public Task<ProfileDto> Get() => profileService.GetAsync(LoggedUserId);

        [HttpPost]
        public Task UpdateUserProfile([FromBody]ProfileInputModel inputModel)
             => profileService.UpdateAsync(
                 inputModel.Name,
                 LoggedUserId,
                 inputModel.Description);
    }
}