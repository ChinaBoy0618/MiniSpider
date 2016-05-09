using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniIOC.Framwork
{
    public interface IEntityFactory
    {
       
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="className"></param>
        /// <returns></returns>
        T CreateInstance<T>(string className) where T : class;
        /// <summary>
        /// 获取实体对象的集合（配置文件中配置的）
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        Dictionary<Type, object> GetInstanceList(string assembly);
    }
}
