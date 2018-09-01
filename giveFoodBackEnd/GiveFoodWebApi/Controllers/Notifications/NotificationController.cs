using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GiveFoodServices.Users;
using GiveFoodServices.Users.Models;
using System;
using GiveFoodWebApi.Controllers.Notifications.Models;
using System.Threading.Tasks;
using GiveFoodDataModels;
using Microsoft.AspNetCore.Authorization;
using GiveFoodWebApi.Authorization;
using GiveFoodWebApi.Controllers.Notifications.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Net.Http;

namespace GiveFoodWebApi.Controllers.Notifications
{
    public class NotificationController : Controller
    {
        private readonly INotificationService notificationService;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<User> userManager;

        public NotificationController(INotificationService notificationService, 
            IAuthorizationService authorizationService,
            UserManager<User> userManager)
        {
            this.notificationService = notificationService;
            this.authorizationService = authorizationService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<NotificationDto> GetAll(Guid userId) =>
            notificationService.GetAllNotification(userId);


        [HttpGet]
        public Task<Notification> Get(long id) =>
            notificationService.GetAsync(id);

        [HttpPost]
        public Task Delete([FromBody]NotificationViewModel inputModel) =>
            notificationService.DeleteAsync(inputModel.Id);

        [HttpPost]
        public Task UpdateRead([FromBody]NotificationViewModel inputModel) =>
            notificationService.UpdateReadAsync(inputModel.Id);


        public async Task<HttpResponseMessage> Authorize(IEnumerable<OperationAuthorizationRequirement> requirements, long id)
        {
            var authorizationHandler = new NotificationAuthorizationHandler(userManager);
            var resource = await notificationService.GetAsync(id);
            var authorizationService = new AuthorizationService<Notification>(authorizationHandler, requirements, resource, HttpContext.User);
            return await authorizationService.Authorize();
        }
    }
}