using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Collections.Generic;
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
            this.requirements = requirements;
            this.resource = resource;
            this.user = user;
        }

        public async Task<bool> IsAuthorized()
        {
            try
            {
                var context = new AuthorizationHandlerContext(requirements, user, resource);
                await authorizationHandler.HandleAsync(context);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
