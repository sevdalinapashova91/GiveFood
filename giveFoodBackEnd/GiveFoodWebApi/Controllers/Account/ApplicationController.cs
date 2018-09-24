using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GiveFoodWebApi.Controllers.Account
{
    public class ApplicationController : Controller
    {
        public string LoggedUserId
        {
            get { return this.User?.Claims?.FirstOrDefault(claim => claim.Type == "sub")?.Value; }

        }
    }
}