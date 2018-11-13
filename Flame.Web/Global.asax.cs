using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Flame.Framework.ViewEngine;
using Flame.Web.Infrastructure;
using Flame.Core;
using FluentScheduler;
using Flame.Framework.Mvc;

namespace Flame.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //初始化
            FlameEngine.Initialize();

            AreaRegistration.RegisterAllAreas();
            //注册插件Route
            RouteResgister.Register(RouteTable.Routes);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //使用自定义的视图引擎
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FlameRazorViewEngine());

            //定时器
            JobManager.Initialize(new TimerDemo());
            
        }
    }
}
