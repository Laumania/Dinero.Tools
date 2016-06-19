using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Dinero.Tools.Difference.Web.Controllers;

namespace Dinero.Tools.Difference.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
