using Flame.Core.Plugins;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;

[assembly: PreApplicationStartMethod(typeof(PluginHelper), "InitPlugins")]
namespace Flame.Core.Plugins
{

    public class PluginHelper
    {
        public const string InstalledFilePath = "~/App_Data/InstallPlugins.txt";
        public const string PluginPath = "~/Plugins";
        public const string PluginDescriptionFile = "Description.json";

        public static List<PluginDescription> InstalledPluginList = new List<PluginDescription>();


        #region 初始化插件 - InitPlugins()
        /// <summary>
        /// 初始化插件
        /// </summary>
        public static void InitPlugins()
        {
            var pluginList = new List<PluginDescription>();
            //找到插件dll存放目录
            string pluginBasePath = CommonHelper.MapPath(PluginPath);

            if (Directory.Exists(pluginBasePath))
            {
                //获取已安装插件列表
                string filePath = CommonHelper.MapPath(InstalledFilePath);
                var installedPlugins = GetInstalledPlugins(filePath);

                //遍历Plugins文件夹下所有的插件文件夹
                var pluginPathList = Directory.GetDirectories(pluginBasePath);
                foreach (var pathItem in pluginPathList)
                {
                    //判断是否存在插件描述文件
                    FileInfo[] files = new DirectoryInfo(pathItem).GetFiles();
                    if (files?.Count() > 0)
                    {
                        var descFile = files.FirstOrDefault(s => s.Name.Equals(PluginDescriptionFile, StringComparison.CurrentCultureIgnoreCase));
                        if (descFile != null)
                        {
                            //获取描述信息
                            var description = JsonConvert.DeserializeObject<PluginDescription>(File.ReadAllText(descFile.FullName));
                            
                            //是否安装
                            description.IsInstalled = installedPlugins.Any(s => s.Equals(description.Name, StringComparison.CurrentCultureIgnoreCase));
                            
                            //找到主程序集，加载到主程序集中
                            FileInfo assemblyFile = files.FirstOrDefault(s => s.Name.Equals(description.AssemblyName, StringComparison.CurrentCultureIgnoreCase));
                            var assembly = Assembly.Load(AssemblyName.GetAssemblyName(assemblyFile.FullName));
                            description.MainAssembly = assembly;
                            BuildManager.AddReferencedAssembly(assembly);
                            
                            //加载其它程序集
                            foreach (var file in files.Where(s => s.Extension.Equals(".dll",StringComparison.CurrentCultureIgnoreCase) && !s.Name.Equals(description.AssemblyName, StringComparison.CurrentCultureIgnoreCase)))
                            {
                                BuildManager.AddReferencedAssembly(Assembly.Load(AssemblyName.GetAssemblyName(file.Name)));
                            }

                            //找到集成IPlugin接口的类
                            description.PluginType = assembly.GetTypes().FirstOrDefault(s => typeof(IPlugins).IsAssignableFrom(s) && !s.IsInterface && s.IsClass && !s.IsAbstract);
                            pluginList.Add(description);
                        }
                    }
                }
                pluginList = pluginList.OrderBy(s => s.Order).ToList();
            }
            InstalledPluginList = pluginList;
        } 
        #endregion

        #region 安装插件 - InstallPlugin(string pluginName)
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
        #endregion

        #region 卸载插件 - UninstallPlugin(string pluginName)
        /// <summary>
        /// 卸载插件
        /// </summary>
        /// <param name="pluginName"></param>
        public static void UninstallPlugin(string pluginName)
        {
            if (string.IsNullOrEmpty(pluginName))
            {
                throw new ArgumentNullException(nameof(pluginName), "插件名不能为空！");
            }
            string filePath = CommonHelper.MapPath(InstalledFilePath);
            if (File.Exists(filePath))
            {
                var installedPlugins = GetInstalledPlugins(filePath);
                //判断是否已经存在此插件名称
                if (installedPlugins.Any(s => s.Equals(pluginName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    installedPlugins.Remove(pluginName);
                    //保存到文件
                    SaveInstalledPlugins(installedPlugins, filePath);
                }
            }
        }
        #endregion

        #region 是否安装 - IsInstall(string pluginName)
        /// <summary>
        /// 是否安装
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <returns></returns>
        public static bool IsInstall(string pluginName)
        {
            if (string.IsNullOrEmpty(pluginName))
            {
                throw new ArgumentNullException(nameof(pluginName), "插件名不能为空！");
            }
            string filePath = CommonHelper.MapPath(InstalledFilePath);
            if (File.Exists(filePath))
            {
                var installedPlugins = GetInstalledPlugins(filePath);
                //判断是否存在此插件名称
                if (installedPlugins.Any(s => s.Equals(pluginName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    return true;
                }
            }
            return false;
        } 
        #endregion

        #region 公共方法
        #region 获取已安装的插件 - GetInstalledPlugins(string filePath)
        /// <summary>
        /// 获取已安装的插件
        /// </summary>
        /// <returns></returns>
        private static List<string> GetInstalledPlugins(string filePath)
        {
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
                    if (!string.IsNullOrEmpty(result) && !string.IsNullOrWhiteSpace(result))
                    {
                        list.Add(result.Trim());
                    }
                }
            }
            return list;
        }
        #endregion

        #region 保存已安装的插件 - SaveInstalledPlugins(List<string> installPlugins, string path)
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
        #endregion 
        #endregion
    }
} 
