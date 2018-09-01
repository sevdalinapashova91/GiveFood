using GiveFoodDataModels;
using GiveFoodServices.Users.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace GiveFoodServices.Users
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailService emailService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
        }

        public async Task RegisterAsync(UserDto userDto)
        {
            var user = new User { UserName = userDto.Email, Email = userDto.Email };
            var result = await userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {

                //var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = new UrlHelper().Page(
                //        "/Account/ConfirmEmail",
                //        pageHandler: null,
                //        values: new { userId = user.Id, code = code },
                //        protocol: UvRequest.Scheme);
                //var test = await emailService.SendEmailAsync(userDto.Email, "Confirm your email",
                //$"Please confirm your account by ");
            }
        }

        public Task LogoutAsync()
        {
            return signInManager.SignOutAsync();
        }

        public async Task LoginAsync(LoginDto loginDto)
        {
            var result = await signInManager
                 .PasswordSignInAsync(
                    loginDto.Email,
                    loginDto.Password,
                    loginDto.RememberMe,
                    lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Invalid username or password!");
            }
        }
    }
}
