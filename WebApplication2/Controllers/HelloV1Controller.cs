using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    [RoutePrefix("v1/hello")]
    public class HelloV1Controller : ApiController
    {
        // GET: api/HelloV1
  
        [Route("")]
        [Authorize]
        public IEnumerable<string> Get()
        {
            string id = "id";

            if (User is ClaimsPrincipal)
            {
                var user = User as ClaimsPrincipal;
                var claims = user.Claims.ToList();

                id = claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            }

            return new string[] { "value1", id };
        }

        // GET: api/HelloV1/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/HelloV1
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/HelloV1/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/HelloV1/5
        public void Delete(int id)
        {
        }
    }
}
