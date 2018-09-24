using GiveFoodServices.Admin;
using GiveFoodServices.Admin.Models;
using GiveFoodWebApi.Authorization;
using GiveFoodWebApi.Controllers.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Controllers.Admin
{
    [Authorize(Roles = RoleConstants.Admin)]
    public class AdminController : ApplicationController
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public IEnumerable<UserDto> GetAll() =>
            adminService.GetAllUsers(Guid.Parse(LoggedUserId));


        [HttpGet]
        public Task<UserDto> Get(string email) =>
            adminService.GetUserAsync(email);


        [HttpPost]
        public async Task EvaluateUser([FromBody]EvaluateUserDto evaluateUserDto) =>
            await adminService.EvaluateUserAsync(evaluateUserDto);
    }
}