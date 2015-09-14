using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace WebApi1.Provides
{
    public class JwtOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }


        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            // TODO: verify user

            // issuse identity
            var claimsIdentity = new ClaimsIdentity("JWT");
            claimsIdentity.AddClaim(new Claim("userId", Guid.NewGuid().ToString()));
            claimsIdentity.AddClaim(new Claim("name", "Jason Lee"));
            claimsIdentity.AddClaim(new Claim("email", "test@test.com"));
            claimsIdentity.AddClaim(new Claim("phone", "0123456789"));
            claimsIdentity.AddClaim(new Claim("roles", "Manager,Supervisor"));

            var ticket = new AuthenticationTicket(claimsIdentity, null);

            context.Validated(ticket);

        }
    }
}