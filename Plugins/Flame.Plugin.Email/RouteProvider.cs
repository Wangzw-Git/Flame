using Flame.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Flame.Plugin.Email
{
    public class RouteProvider : IRouteProvider
    {
        //注册路由规则
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Flame.Plugin.Email",
                "Plugins/Email/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Flame.Plugin.Email" });
        }
    }
}