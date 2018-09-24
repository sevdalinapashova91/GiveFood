using GiveFoodDataModels;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveFoodIdentityServer
{
    public class IdentityWithAdditionalClaimsProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;
        private readonly UserManager<User> _userManager;

        public IdentityWithAdditionalClaimsProfileService(
            UserManager<User> userManager, 
            IUserClaimsPrincipalFactory<User> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.UserName));

            if (user.Type == UserType.Admin)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "Admin"));
            }
            //TODO:add here check for role 
            else if((user.Type == UserType.Giver || user.Type == UserType.Taker)&& user.Status == UserStatus.Approved)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, user.Type.ToString() ));
            }

 
            claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, user.Email));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
