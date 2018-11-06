using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Mvc;
using Flame.Data;
using System.Web.Mvc;
using Flame.Core.Plugins;
using System.Reflection;
using Flame.Core.Dependency;

namespace Flame.Core
{
    public static class FlameEngine
    {
        /// <summary>
        /// 项目初始化
        /// </summary>
        public static void Initialize()
        {
            //注册
            Regisgter();
        }

        private static void Regisgter()
        {
            var builder = new ContainerBuilder();
            //找到实现IDependencyRegister接口的类，并调用Register方法
            var assemblys = AppDomain.CurrentDomain.GetAssemblies().Where(s=>s.FullName.StartsWith("Flame."));
            foreach (var item in assemblys)
            {
                var registerTypeList = item.GetTypes().Where(s => s.BaseType == typeof(IDependencyRegister));
                foreach (var registerType in registerTypeList)
                {
                    var instance = (IDependencyRegister)Activator.CreateInstance(registerType);
                    instance.Register(builder);
                }
            }

            //单例
            builder.RegisterType<FlameContext>().As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<BasePlugin>().As<IPlugins>().InstancePerLifetimeScope();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
