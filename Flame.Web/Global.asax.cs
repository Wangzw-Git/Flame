using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Flame.Framework.ViewEngine;

namespace Flame.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //使用自定义的视图引擎
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FlameRazorViewEngine());
        }
    }
}
