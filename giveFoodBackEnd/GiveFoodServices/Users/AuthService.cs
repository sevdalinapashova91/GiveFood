using GiveFoodDataModels;
using GiveFoodServices.Users.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text.Encodings.Web;
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

        public async Task Register(UserDto userDto)
        {
            var user = new User { UserName = userDto.Email, Email = userDto.Email };
            var result = await userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {

                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var callBackUrl = "";
                await emailService.SendEmailAsync(userDto.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callBackUrl)}'>clicking here</a>.");
            }
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task Login(LoginDto loginDto)
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
