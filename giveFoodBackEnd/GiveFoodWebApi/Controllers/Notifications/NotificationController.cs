using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GiveFoodServices.Users;
using GiveFoodServices.Users.Models;
using System;
using GiveFoodWebApi.Controllers.Notifications.Models;
using System.Threading.Tasks;
using GiveFoodWebApi.Authorization;
using GiveFoodWebApi.Controllers.Notifications.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Net.Http;
using GiveFoodWebApi.Controllers.Account;
using Microsoft.AspNetCore.Authorization;

namespace GiveFoodWebApi.Controllers.Notifications
{
    [Authorize]
    public class NotificationController : ApplicationController
    {
        private readonly INotificationService notificationService;
        private readonly INotificationAuthorizationService authorizationService;

        public NotificationController(INotificationService notificationService,
            INotificationAuthorizationService authorizationService)
        {
            this.notificationService = notificationService;
            this.authorizationService = authorizationService;
        }

        [HttpGet]
        public IEnumerable<NotificationDto> GetAll() =>
            notificationService.GetAllNotification(Guid.Parse(LoggedUserId));

        [HttpGet]
        public IEnumerable<NotificationDto> GetUnread() => 
             notificationService.GetUnread(Guid.Parse(LoggedUserId));
        

        [HttpGet]
        public async Task<HttpResponseMessage> Get(long id) =>
           await authorizationService.ExecuteWithAuthorization(
                new List<OperationAuthorizationRequirement>() { AuthorizationOperations.Read },
                id,
                User,
                () => Ok(notificationService.GetAsync(id)));
    

        [HttpPost]
        public Task<HttpResponseMessage> Delete([FromBody]NotificationViewModel inputModel) =>
          authorizationService.ExecuteWithAuthorization(
                new List<OperationAuthorizationRequirement>() { AuthorizationOperations.Delete },
                inputModel.Id,
                 User,
                 () => notificationService.DeleteAsync(inputModel.Id));


        [HttpPost]
        public Task<HttpResponseMessage> UpdateRead([FromBody]NotificationViewModel inputModel) =>
            authorizationService.ExecuteWithAuthorization(
                 new List<OperationAuthorizationRequirement>() { AuthorizationOperations.Update },
                 inputModel.Id,
                 HttpContext.User,
                () => notificationService.UpdateReadAsync(inputModel.Id));

    }
}