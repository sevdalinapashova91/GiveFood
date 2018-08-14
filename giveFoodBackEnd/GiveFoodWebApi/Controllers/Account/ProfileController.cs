
using Microsoft.AspNetCore.Mvc;

namespace GiveFoodWebApi.Controllers.Account
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public ProfileViewModel Get(string userId)
        {
            return null;
        }

        [HttpPost]
        public void CreateProfile(ProfileViewModel profileData) { }
    }
}