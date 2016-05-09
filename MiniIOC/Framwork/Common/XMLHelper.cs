using System;
using System.IO;
using System.Xml.Serialization;


namespace MiniIOC.Framwork.Common
{
    public class XMLHelper
    {
        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>返回实体对象</returns>
        public static T Deserialize<T>(string xml) where T:class
        {
            if (string.IsNullOrEmpty(xml))
                return null;
            
            try
            {
                using(StringReader stream=new StringReader(xml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(stream);
                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serializer<T>(T obj)
        {
            MemoryStream stream = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StreamReader sr = null; string res = string.Empty;
            try
            {
                serializer.Serialize(stream, obj);
                sr = new StreamReader(stream);
                res= sr.ReadToEnd();
                sr.Dispose();
                stream.Dispose();
            }
            catch
            {
                if (sr!=null)
                    sr.Dispose();
                stream.Dispose();
            }
            
            return res;
        }
    }
}
