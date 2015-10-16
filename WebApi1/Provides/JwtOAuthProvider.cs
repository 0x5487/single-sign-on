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
            //if (context.UserName == "jason" && context.Password == "123123")
            //{
                // issuse identity
                var claimsIdentity = new ClaimsIdentity("jwt");
                claimsIdentity.AddClaim(new Claim("sid", Guid.NewGuid().ToString()));
                claimsIdentity.AddClaim(new Claim("userId", "1"));
                claimsIdentity.AddClaim(new Claim("name", "Jason Lee"));
                claimsIdentity.AddClaim(new Claim("email", "test@test.com"));
                claimsIdentity.AddClaim(new Claim("country_code", "886"));
                claimsIdentity.AddClaim(new Claim("phone", "911234567"));
                claimsIdentity.AddClaim(new Claim("role", "admin"));
                claimsIdentity.AddClaim(new Claim("role", "organizer"));

            var ticket = new AuthenticationTicket(claimsIdentity, null);

                context.Validated(ticket);
            //}




        }
    }
}