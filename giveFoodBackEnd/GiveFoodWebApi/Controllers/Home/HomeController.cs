using Microsoft.AspNetCore.Mvc;

namespace GiveFoodWebApi.Controllers.Home
{
    public class HomeController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return "I am the home page";
        }
    }
}