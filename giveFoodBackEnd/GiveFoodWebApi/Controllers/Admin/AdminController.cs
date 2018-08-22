using GiveFoodDataModels;
using GiveFoodServices.Admin;
using GiveFoodServices.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpPost]
        public async Task EvaluateUser([FromBody]EvaluateUserDto evaluateUserDto)
        {
            await adminService.EvaluateUser(evaluateUserDto);
        }
    }
}