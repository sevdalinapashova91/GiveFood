using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace GiveFoodWebApi.Controllers.Notifications.Authorization
{
    public interface INotificationAuthorizationService
    {
        Task<HttpResponseMessage> ExecuteWithAuthorization(IEnumerable<OperationAuthorizationRequirement> requirements, long id, ClaimsPrincipal user, Action action);
        Task<HttpResponseMessage> ExecuteWithAuthorization(IEnumerable<OperationAuthorizationRequirement> requirements, long id, ClaimsPrincipal user, Func<HttpResponseMessage> action);
    }
}