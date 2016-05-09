using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace MiniIOC
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 获取配置节
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetAppSettings(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    
    }
}
