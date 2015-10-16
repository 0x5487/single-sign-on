using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace WebApi3.Controllers
{
    [Route("v1/status")]
    public class StatusController : Controller
    {
        // GET api/values/5
        //[Authorize("Bearer")]
        [Authorize]
        [HttpGet("")]
        public string OK()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {

            }

            return "ok";
        }

        [HttpGet("info")]
        public string GetInfo()
        {
            return "info";
        }


    }
}
