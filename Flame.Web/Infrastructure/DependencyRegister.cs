using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Flame.Services;
using System.Reflection;
using Flame.Core.Dependency;

namespace Flame.Web.Infrastructure
{
    public class DependencyRegister : IDependencyRegister
    {
        public void Register(ContainerBuilder builder)
        {
            //注册所有Controller
            builder.RegisterTypes(Assembly.GetExecutingAssembly().GetTypes().Where(s => s.Name.EndsWith("Controller")).ToArray());

            //注册 services
            builder.RegisterType<TimerSchedulerServices>().As<ITimerSchedulerServices>().InstancePerLifetimeScope();
        }
    }
}