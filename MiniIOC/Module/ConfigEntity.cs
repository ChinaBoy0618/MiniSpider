using System;
using System.Xml.Serialization;

namespace MiniIOC.Module
{
    /// <summary>
    /// 配置实体
    /// </summary>
    [XmlRoot("InstanceConfig")]
    [Serializable]
    public class ConfigEntity
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string Assembly;
        /// <summary>
        /// 类名（包含命名空间）
        /// </summary>
        public string ClassName;
    }
}
