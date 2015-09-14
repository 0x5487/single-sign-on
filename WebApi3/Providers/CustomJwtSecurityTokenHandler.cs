using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication;
using Newtonsoft.Json;

namespace WebApi3.Providers
{
    public class CustomJwtSecurityTokenHandler : ISecurityTokenValidator
    {
        public bool CanReadToken(string securityToken) => true;

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {

            //eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1bmlxdWVfbmFtZSI6Ikphc29uIExlZSIsInN1YiI6Ikphc29uIExlZSIsInJvbGUiOlsiTWFuYWdlciIsIlN1cGVydmlzb3IiXSwiaXNzIjoiaHR0cDovL2p3dGF1dGh6c3J2LmF6dXJld2Vic2l0ZXMubmV0IiwiYXVkIjoiUm9ja2V0IiwiZXhwIjoxNDQxOTgwMjE5LCJuYmYiOjE0NDE5NzY2MTl9.yegylhGkz5uasu5E--aEbCAHfi5aE9Z17_pZAE63Bog

            validatedToken = null;


            var key = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw";
            
            try
            {
                var raw = JsonWebToken.Decode(securityToken, key);

                var payLoad = JsonConvert.DeserializeObject<IDictionary<string, string>>(raw);

                var claims = new List<Claim>();

                foreach (var row in payLoad)
                {
                    var claim = new Claim(row.Key, row.Value);
                    claims.Add(claim);
                }

                var claimsIdentity = new ClaimsIdentity(claims, "JWT");

                return new ClaimsPrincipal(claimsIdentity);
            }
            catch (Exception ex)
            {
                return null;

            }
            
        }

        public bool CanValidateToken { get; } 
        public int MaximumTokenSizeInBytes { get; set; }
    }
}
