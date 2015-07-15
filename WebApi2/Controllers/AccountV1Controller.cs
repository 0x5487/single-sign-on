using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("v1/account")]
    public class AccountV1Controller : ApiController
    {
        [Route("ping")]
        [HttpGet]
        public string Ping()
        {
            return "ping2";
        }

        [HttpGet]
        [Authorize]
        [Route("secret")]
        public IEnumerable<string> Get()
        {
            return new string[] { "WebApi2" };
        }

        // GET: api/Account/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Account
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Account/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Account/5
        public void Delete(int id)
        {
        }
    }
}
