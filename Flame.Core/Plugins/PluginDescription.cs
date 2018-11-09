using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Core.Plugins
{
    public class PluginDescription
    {
        public string Name { get; set;}

        public string Version { get; set; }

        public string Description { get; set; }

        public int Order { get; set;}

        public bool IsInstalled { get; set; }
    }
}
