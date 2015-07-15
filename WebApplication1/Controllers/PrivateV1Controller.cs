using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    [RoutePrefix("v1/private")]
    public class PrivateV1Controller : ApiController
    {
        [HttpGet]
        [Authorize]
        [Route("")]
        public string Index()
        {
            return "Hello";
        }


    }
}
