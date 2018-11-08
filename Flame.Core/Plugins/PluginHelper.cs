using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Core.Plugins
{
    public class PluginHelper
    {
        public const string InstalledFilePath = "~/App_Data/InstallPlugins.txt";
        public const string PluginPath = "~/Plugins";


        /// <summary>
        /// 安装插件
        /// </summary>
        /// <param name="pluginName"></param>
        public static void InstallPlugin(string pluginName)
        {
            if (string.IsNullOrEmpty(pluginName))
            {
                throw new ArgumentNullException(nameof(pluginName), "插件名不能为空！");
            }
            //安装插件文件
            //不存在就创建
            string filePath = CommonHelper.MapPath(InstalledFilePath);
            if (!File.Exists(filePath))
            {
                var fileStream = File.Create(filePath);
                fileStream.Dispose();
            }
            var installedPlugins = GetInstalledPlugins(filePath);
            //判断是否已经存在此插件名称
            if (!installedPlugins.Any(s => s.Equals(pluginName, StringComparison.CurrentCultureIgnoreCase)))
            {
                installedPlugins.Add(pluginName);
                //保存到文件
                SaveInstalledPlugins(installedPlugins, filePath);
            }
        }

        /// <summary>
        /// 获取已安装的插件
        /// </summary>
        /// <returns></returns>
        private static List<string> GetInstalledPlugins(string filePath)
        {
            //string filePath = CommonHelper.MapPath(InstalledFilePath);
            if (!File.Exists(filePath))
            {
                return new List<string>();
            }
            //读取文件
            var content = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(content))
            {
                return new List<string>();
            }
            var list = new List<string>();
            using (var reader = new StringReader(content))
            {
                string result = string.Empty;
                //每次取一行数据，直至取完
                while ((result = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(result)&&!string.IsNullOrWhiteSpace(result))
                    {
                        list.Add(result.Trim());
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 保存已安装的插件
        /// </summary>
        /// <param name="installPlugins">已安装的插件</param>
        /// <param name="path">文件路径</param>
        private static void SaveInstalledPlugins(List<string> installPlugins, string path)
        {
            if (installPlugins?.Count > 0)
            {
                string plugins = string.Empty;
                installPlugins.ForEach(s => plugins += s + Environment.NewLine);
                File.WriteAllText(path, plugins);
            }
        }
    }
}
