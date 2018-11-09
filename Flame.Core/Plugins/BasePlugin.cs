using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Core.Plugins
{
    public abstract class BasePlugin : IPlugins
    {
        public virtual PluginDescription PluginDescriptor { get; set;}

        public virtual void Install()
        {
            PluginHelper.InstallPlugin(PluginDescriptor.SystemName);
        }
        
        public virtual void UnInstall()
        {
            PluginHelper.UninstallPlugin(PluginDescriptor.SystemName);
        }
    }
}
