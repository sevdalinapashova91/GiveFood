using GiveFoodDataModels;
using GiveFoodWebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Controllers.Notifications.Authorization
{
    public class NotificationAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Notification>
    {
        private readonly UserManager<User> userManager;

        public NotificationAuthorizationHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            OperationAuthorizationRequirement requirement, 
            Notification resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask; 
            }

            if (requirement.Name != AuthorizationConstants.CreateOperationName &&
                requirement.Name != AuthorizationConstants.ReadOperationName &&
                requirement.Name != AuthorizationConstants.UpdateOperationName &&
                requirement.Name != AuthorizationConstants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            var userId = context.User.Claims.FirstOrDefault(x => x.Type == "sub").Value;

            if (resource.SendTo == Guid.Parse(userId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
