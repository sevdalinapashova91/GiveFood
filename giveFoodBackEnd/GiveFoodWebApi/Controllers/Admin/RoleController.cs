using GiveFoodServices.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Controllers.Admin
{
    //[Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        //add attribute for admin role only
        private readonly IRoleService roleService;
        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task AssignGiverRole(string userId)
        {
            await roleService.AssignRoleToUser(userId, Role.Giver.ToString());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task AssignTakerRole(string userId)
        {
            await roleService.AssignRoleToUser(userId, Role.Taker.ToString());
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task CreateRoles()
        {
            await roleService.CreateRolesandAdminUser();
        }
       
    }
}