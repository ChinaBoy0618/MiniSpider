using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace MiniIOC.Framwork.Config
{
    /// <summary>
    /// 实现自定义节点
    /// </summary>
    public class MiniIocConfigurationSectionHandler:IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            NameValueCollection collection;
            NameValueSectionHandler baseHandler = new NameValueSectionHandler();
            var obj=baseHandler.Create(parent, configContext, section);
            return obj;
        }
    }
}
