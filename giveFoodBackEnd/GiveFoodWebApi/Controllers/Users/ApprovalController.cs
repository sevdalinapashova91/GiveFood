using GiveFoodDataModels;
using GiveFoodServices.Users;
using GiveFoodServices.Users.Models;
using GiveFoodWebApi.Authorization;
using GiveFoodWebApi.Controllers.Account;
using GiveFoodWebApi.Controllers.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Controllers.Users
{   
    public class ApprovalController : ApplicationController
    {
        private readonly IUserService userService;
        private readonly IApprovalService approvalService;
        private readonly UserManager<User> userManager;

        public ApprovalController(
            IUserService userService,
            IApprovalService approvalService,
            UserManager<User> userManager)
        {
            this.userService = userService;
            this.approvalService = approvalService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Giver)]
        public Task<IEnumerable<UserInRoleDto>> GetTakers() =>
                this.userService.GetTakersAsync();

        [HttpGet]
        [Authorize(Roles = RoleConstants.Taker)]
        public Task<IEnumerable<UserInRoleDto>> GetGivers() =>
           this.userService.GetGiversAsync();

        [HttpPost]
        [Authorize(Roles = RoleConstants.Giver)]
        public Task RequestApprovalToGive([FromBody]ApprovalViewModel inputModel) => 
            approvalService.RequestApproval(inputModel.Email, UserType.Taker, Guid.Parse(LoggedUserId));

        [HttpPost]
        [Authorize(Roles = RoleConstants.Taker)]
        public Task RequestApprovalToTake([FromBody]ApprovalViewModel inputModel) =>
           approvalService.RequestApproval(inputModel.Email, UserType.Giver, Guid.Parse(LoggedUserId));

        [HttpPost]
        public Task RejectApproval([FromBody]NotificationApprovalViewModel inputModel) =>
            approvalService.Reject(inputModel.Id, Guid.Parse(LoggedUserId));

        [HttpPost]
        public Task Approve([FromBody]NotificationApprovalViewModel inputModel) => 
            approvalService.Approve(inputModel.Id, Guid.Parse(LoggedUserId));
    }
}