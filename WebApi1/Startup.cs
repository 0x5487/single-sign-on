using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using WebApi1.Provides;
using WebApi3.Providers;

[assembly: OwinStartup(typeof(WebApi.Startup))]
namespace WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);

        }


        public void ConfigureOAuth(IAppBuilder app)
        {
            string privatekey = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw";

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(90),
                Provider = new JwtOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat(privatekey)
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
                AccessTokenFormat = new CustomJwtFormat(privatekey),
            });
        }
    }



    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });


            if (context.UserName == "jason" && context.Password == "123123")
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
        }
    }
}
