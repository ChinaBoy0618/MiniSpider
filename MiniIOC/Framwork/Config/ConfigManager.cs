using System;
using System.Collections.Specialized;
using System.Configuration;

namespace MiniIOC.Framwork.Config
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public class ConfigManager
    {
        private static readonly string sectionName = "MiniIOC";
        private static MiniIOCSection _miniIOCSection;

        static ConfigManager()
        {
            _miniIOCSection = ConfigurationManager.GetSection(sectionName) as MiniIOCSection??new MiniIOCSection();
        }
        /// <summary>
        /// 获取配置节
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetMiniIOCSettings(string name)
        {
            switch (name)
            {
                case "AssemblyPath":
                    return _miniIOCSection.AssemblyPath;
                case "XmlCollectionPath" :
                    return _miniIOCSection.XmlCollectionPath;
                case "MiniIOCEntitys":
                    return _miniIOCSection.MiniIOCEntitys;

                default:
                    return string.Empty;
            }
        }

        public static string XmlCollectionPath
        {
            get
            {
                return GetMiniIOCSettings("XmlCollectionPath") as string;
            }
        }

        public static string AssemblyPath
        {
            get
            {
                return GetMiniIOCSettings("AssemblyPath") as string;
            }
        }

        public static MiniIOCEntitysCollection MiniIOCEntitys
        {
            get
            {
                return GetMiniIOCSettings("MiniIOCEntitys") as MiniIOCEntitysCollection;
            }
        }

        public static string CouldNotResolveType
        {
            get
            {
                return GetMiniIOCSettings("CouldNotResolveType") as string ?? "CouldNotResolveType";
            }
        }
           
    }
}
