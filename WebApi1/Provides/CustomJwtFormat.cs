using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using Newtonsoft.Json;
using Thinktecture.IdentityModel.Tokens;
using WebApi3.Providers;



namespace WebApi1.Provides
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {

        private readonly string _secretKey = string.Empty;

        public CustomJwtFormat(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            var exp = data.Properties.ExpiresUtc.Value.ToEpochTime();

            var payLoad = new Dictionary<string, string>();
            payLoad.Add("exp", exp.ToString());

            foreach (var claim in data.Identity.Claims)
            {
                payLoad.Add(claim.Type, claim.Value);
            }

            var result = JsonWebToken.Encode(payLoad, _secretKey, JwtHashAlgorithm.HS256);
            return result;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            try
            {
                var raw = JsonWebToken.Decode(protectedText, _secretKey);
                var payLoad =JsonConvert.DeserializeObject<IDictionary<string, string>>(raw);

                var claims = new List<Claim>();

                foreach (var row in payLoad)
                {
                    var claim = new Claim(row.Key, row.Value);
                    claims.Add(claim);
                }

                var ci = new ClaimsIdentity(claims, "JWT");
                return new AuthenticationTicket(ci, new AuthenticationProperties());
            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}