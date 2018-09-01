using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Authorization
{

    public class AuthorizationService<T> : IAuthorizeService
    {
        private readonly IAuthorizationHandler authorizationHandler;
        private readonly IEnumerable<OperationAuthorizationRequirement> requirements;
        private readonly T resource;
        private readonly ClaimsPrincipal user;

        public AuthorizationService(
            IAuthorizationHandler authorizationHandler,
            IEnumerable<OperationAuthorizationRequirement> requirements,
            T resource,
            ClaimsPrincipal user
            )
        {
            this.authorizationHandler = authorizationHandler;
            this.resource = resource;
            this.user = user;
        }

        public async Task<HttpResponseMessage> Authorize()
        {
            try
            {
                var context = new AuthorizationHandlerContext(requirements, user, resource);
                await authorizationHandler.HandleAsync(context);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}
