using GiveFoodDataModels;
using GiveFoodServices.Users;
using GiveFoodWebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Controllers.Notifications.Authorization
{
    public class NotificationAuthorizationService : INotificationAuthorizationService
    {
        private readonly UserManager<User> userManager;
        private readonly INotificationService notificationService;

        public NotificationAuthorizationService(
            UserManager<User> userManager,
            INotificationService notificationService)
        {

            this.userManager = userManager;
            this.notificationService = notificationService;
        }

        private async Task<bool> IsNotificationAuthorized(IEnumerable<OperationAuthorizationRequirement> requirements, long id, ClaimsPrincipal user)
        {
            var authorizationHandler = new NotificationAuthorizationHandler(userManager);
            var resource = await notificationService.GetAsync(id);
            var authorizationService = new AuthorizationService<Notification>(authorizationHandler, requirements, resource, user);
            return await authorizationService.IsAuthorized();
        }

        public async Task<HttpResponseMessage> ExecuteWithAuthorization(
            IEnumerable<OperationAuthorizationRequirement> requirements,
            long id,
            ClaimsPrincipal user,
            Action action)
        {
            if (await IsNotificationAuthorized(requirements, id, user))
            {
                action();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.Forbidden);
        }

        public async Task<HttpResponseMessage> ExecuteWithAuthorization(
            IEnumerable<OperationAuthorizationRequirement> requirements,
            long id,
            ClaimsPrincipal user,
            Func<HttpResponseMessage> action)
        {
            if (await IsNotificationAuthorized(requirements, id, user))
            {
                return action();
            }

            return new HttpResponseMessage(HttpStatusCode.Forbidden);
        }
    }
}
