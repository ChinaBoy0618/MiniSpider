using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniIOC.Framwork.Config;
using MiniIOC.Framwork.Common;

namespace MiniIOC.Framwork
{
    public class EntityFactory : IEntityFactory
    {
        public static IEntityFactory Instance
        {
            get {
                return Singleton<EntityFactory>.Instance;
            }
        }
         
        /// <summary>
        /// 实体
        /// </summary>
        public static Dictionary<string, string> infos = new Dictionary<string, string>();
        public EntityFactory()
        { 
            //初始化配置的对象
           
        }
        public T CreateInstance<T>(string className) where T : class
        {


            return null;
        }
        public Dictionary<Type, object> GetInstanceList(string assembly)
        {
            throw new NotImplementedException();
        }
    }
}
