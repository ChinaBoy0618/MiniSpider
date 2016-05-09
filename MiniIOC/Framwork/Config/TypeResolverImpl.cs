using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using MiniIOC.Framwork.AssemblyLoader;
using MiniIOC.Framwork.Common;
/*
 * 先完成ioc，之后再说依赖注入
 * 先不考虑容错机制
 */
namespace MiniIOC.Framwork.Config
{
    public class TypeResolverImpl
    {
        private static Dictionary<string, Tuple<Tuple<string, string>, Tuple<string, string>>> classAndAssemblys = new Dictionary<string, Tuple<Tuple<string, string>, Tuple<string, string>>>();
        private static Dictionary<string, Type> instances = new Dictionary<string,Type>();

        public TypeResolverImpl()
        {
            MiniIOCEntitysCollection collection = ConfigManager.MiniIOCEntitys;
            //var className = string.Empty;

            foreach (EntityElement entity in collection)
            {
                Tuple<string, string> type = GetTuple(entity.Type), mapto = GetTuple(entity.MapTo);
                classAndAssemblys.Add(type.Item1, new Tuple<Tuple<string, string>, Tuple<string, string>>(type, mapto));
            }
        }

        public Type ResolveType(string className)
        { 
          if(!classAndAssemblys.ContainsKey(className))
                return null;
            switch (ConfigManager.AssemblyPath)
            {
                case "":
                   return instances.ContainsKey(className)?instances[className]:ResolveDefaultType(className);
           
                default://加载程序集 
                   AssemblyLoader.AssemblyLoader assembly = Singleton<AssemblyLoader.AssemblyLoader>.Instance;
                   //assembly.TypesOf();
                    return null;
            }
        }
        public Type ResolveType(Type type)
        {
            return ResolveType(type.FullName);
        }
        private Tuple<string, string> GetTuple(string tupleString)
        {
            if (!string.IsNullOrEmpty(tupleString))
            {
                var element = tupleString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //不满足条件的配置会被过滤掉
                if (element.Length == 2)
                    return new Tuple<string, string>(element[0], element[1]);
            }
            return null;
        }
        public string GetTypeNameSpaceAndAssembly(Tuple<string, string> type)
        {
            return string.Format("{0},{1}", type.Item1, type.Item2);
        }
        public Type ResolveDefaultType(string className)
        { 
            if(string.IsNullOrEmpty(className)) return null;
            Type type = null;
            try
            {
                type = Type.GetType(GetTypeNameSpaceAndAssembly(classAndAssemblys[className].Item2));
                if (!instances.ContainsKey(className) && type != null)
                    instances.Add(className, type);//现在默认名字就是命名空间+classname
                return type;
            }
            catch
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ConfigManager.CouldNotResolveType, new object[] { className })); ;
            }
           
        }
      
    }
}
