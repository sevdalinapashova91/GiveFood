using GiveFoodDataModels;
using GiveFoodServices.Users;
using GiveFoodServices.Users.Models;
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
        public async Task Register([FromBody]UserDto userDto)
        {
            await this.authService.Register(userDto);
        }

        [HttpPost]
        public async Task LogIn([FromBody]LoginDto loginInfo)
        {
            await this.authService.Login(loginInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task LogOut()
        {
            await this.authService.Logout();
        }

        [HttpGet]
        public string AccessDenied()
        {
            return "You are not autorized!";
        }
    }
}