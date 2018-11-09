using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Core.Plugins
{
    public interface IPlugins
    {
        /// <summary>
        /// 插件描述信息
        /// </summary>
        PluginDescription PluginDescriptor { get; set; }

        /// <summary>
        /// 安装
        /// </summary>
        void Install();

        /// <summary>
        /// 卸载
        /// </summary>
        void UnInstall();
    }
}
