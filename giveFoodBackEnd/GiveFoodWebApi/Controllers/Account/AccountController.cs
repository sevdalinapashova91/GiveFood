using GiveFoodServices.Users;
using GiveFoodServices.Users.Models;
using GiveFoodWebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IAuthService authService;      

        public AccountController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public Task Register([FromBody]UserDto userDto) =>
             this.authService.RegisterAsync(userDto);

    }
    public class IdentityController : ApplicationController
    {
        private readonly IProfileService profileService;
        public IdentityController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAuthContext() => Ok(new AuthContext
        {
            UserProfile = await this.profileService.GetAsync(LoggedUserId),
            Claims = User.Claims
                .Select(claim => new SimpleClaim
                {
                    Type = claim.Type,
                    Value = claim.Value
                })
                .ToList()
        });
    }
}

