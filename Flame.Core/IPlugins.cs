using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Core
{
    public interface IPlugins
    {
        /// <summary>
        /// 安装
        /// </summary>
        void Install();

        /// <summary>
        /// 卸载
        /// </summary>
        void UnInstall();

        /// <summary>
        /// 是否已安装
        /// </summary>
        /// <returns></returns>
        bool IsInstalled();
    }
}
