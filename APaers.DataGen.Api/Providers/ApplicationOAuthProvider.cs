using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;

namespace APaers.DataGen.Api.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            IOwinResponse owinResponse = context.OwinContext.Response;
            owinResponse.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            ApplicationUserManager userManager = context.Request.Context.GetUserManager<ApplicationUserManager>();
            IdentityUser user = userManager.Find(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identity.AddClaim(new Claim("sub", context.UserName));
            IList<string> roles = await userManager.GetRolesAsync(user.Id);
            foreach (string roleName in roles)
                identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
            context.Validated(identity);
        }

        public override async Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            ApplicationUserManager userManager = context.Request.Context.GetUserManager<ApplicationUserManager>();
            IdentityUser user = await userManager.FindByIdAsync(context.Identity.GetUserId());
            if (user != null)
            {
                IList<string> roles = await userManager.GetRolesAsync(user.Id);
                /*context.AdditionalResponseParameters.Add("isUser", roles.Contains(AppConsts.UserRoleName));
                context.AdditionalResponseParameters.Add("isAdmin", roles.Contains(AppConsts.AdminRoleName));*/
            }
        }
    }
}
