using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Flame.Framework
{
    public static class FileHelper
    {
        private static object _lockObj = new object();

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logName">日志文件名称</param>
        /// <param name="content">日志内容</param>
        public static void WriteLog(string logName, string content)
        {
            string fileName = string.Empty;
            string[] logExtension = new string[] { ".txt", ".log" };
            if (!logExtension.Any(s => logName.Contains(s)))
            {
                fileName = logName + ".log";
            }
            else
            {
                fileName = logName;
            }
            string absFolderPath = HttpContext.Current.Server.MapPath("/logs");
            if (!Directory.Exists(absFolderPath))
            {
                Directory.CreateDirectory(absFolderPath);
            }
            string absPath = Path.Combine(absFolderPath, fileName);
            using (var fileStream = File.Open(absPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
            {
                var bytes = Encoding.UTF8.GetBytes(content + "\r\n");
                //将流的起点位置设置在文本最末端
                fileStream.Seek(fileStream.Length, SeekOrigin.Current);
                fileStream.Write(bytes, 0, bytes.Count());
            }
        }

    }
}
