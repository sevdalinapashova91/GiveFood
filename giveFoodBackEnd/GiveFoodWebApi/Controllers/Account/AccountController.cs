using GiveFoodServices.Users;
using GiveFoodServices.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [AllowAnonymous]
        public Task Register([FromBody]UserDto userDto) =>
             this.authService.RegisterAsync(userDto);

        [HttpPost]
        [AllowAnonymous]
        public Task LogIn([FromBody]LoginDto loginInfo) =>
            this.authService.LoginAsync(loginInfo);
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task LogOut() => 
            this.authService.LogoutAsync();
    }
}