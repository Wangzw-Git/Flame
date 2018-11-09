using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Flame.Core.Plugins
{
    public class PluginDescription
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 插件系统名称
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// 插件版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 插件描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否已安装
        /// </summary>
        public bool IsInstalled { get; set; }

        /// <summary>
        /// 主程序集
        /// </summary>
        public Assembly MainAssembly { get; set; }

        /// <summary>
        /// 插件类型
        /// </summary>
        public Type PluginType { get; set; }


        public T Instance<T>() where T : class, IPlugins
        {
            object instance = null;
            Autofac.Integration.Mvc.AutofacDependencyResolver.Current.RequestLifetimeScope.TryResolve(PluginType, out instance);
            var typedInstance = instance as T;
            if (typedInstance != null)
            {
                typedInstance.PluginDescriptor = this;
            }
            return typedInstance;
        }
    }
}
