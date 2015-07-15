using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var auth = Context.Request.Headers["Authorization"];

            if (User != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (Context.User is ClaimsPrincipal)
                    {
                        var user = User as ClaimsPrincipal;
                        var claims = user.Claims.ToList();

                        var nameIdentifier = claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

                    }

                }
            }
        }
    }
}
