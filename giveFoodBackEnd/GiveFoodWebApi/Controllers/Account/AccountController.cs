using GiveFoodDataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        // POST api/values
        [HttpPost]
        public async Task Register([FromBody]UserDto userDto)
        {
            var user = new User { UserName = userDto.Email, Email = userDto.Email};
            var result = await userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                //_logger.LogInformation("User created a new account with password.");

                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                await signInManager.SignInAsync(user, isPersistent: false);
                //_logger.LogInformation("User created a new account with password.");
               // return RedirectToLocal(returnUrl);
            }
        }

        [HttpPost]
        public async Task LogIn([FromBody]LoginDto loginInfo)
        {
            var result = await signInManager.PasswordSignInAsync(loginInfo.Email, loginInfo.Password, loginInfo.RememberMe, lockoutOnFailure: false);
            //throw if not ok result?
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task LogOut(string name, string password)
        {
            await signInManager.SignOutAsync();
        }
        [HttpGet]
        public string AccessDenied()
        {
            return "You are not autorized!";
        }
    }
}