using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Core.Plugins
{
    public class BasePlugin : IPlugins
    {
        public BasePlugin()
        {

        }


        public void Install()
        {
            throw new NotImplementedException();
        }

        public bool IsInstalled()
        {
            throw new NotImplementedException();
        }

        public void UnInstall()
        {
            throw new NotImplementedException();
        }
    }
}
