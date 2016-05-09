using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using MiniIOC.Framwork.Config;

//加载程序集
namespace MiniIOC.Framwork.AssemblyLoader
{
    internal class AssemblyLoader
    {
      private static string _assemblyPath;
        private static IEnumerable<Type> _types = null;
        public Assembly AssemblyPath
        {
            get {

                var assembly = Assembly.LoadFile(_assemblyPath);
                if (assembly == null)
                    throw new AssemblyException(_assemblyPath);
                
                return  Assembly.LoadFile(_assemblyPath);
            }
        }
        public IEnumerable<Type> Types
        {
            get
            {
                if (_types == null)
                {
                    DirectoryInfo dir = new DirectoryInfo(ConfigManager.AssemblyPath ?? GetAssemblyDric());
                    //if (!dir.Exists) dir = new DirectoryInfo(HostingEnvronment.GetMapPath("/"));
                    var searchFiles = dir.GetFiles("*.dll", SearchOption.AllDirectories);
                    var TypeList = searchFiles.SelectMany(f => AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(f.FullName)).GetTypes());
                    _types = TypeList;
                }
                return _types;
            }
        }
        public AssemblyLoader():this(AppDomain.CurrentDomain.BaseDirectory)
        { 
        }
        public AssemblyLoader(string path)
        {
            _assemblyPath = path;
        }  

        public string GetAssemblyDric()
        {
            return _assemblyPath.IndexOf('.') > -1 ? _assemblyPath.Substring(0, _assemblyPath.LastIndexOf('/') > 0 ? _assemblyPath.LastIndexOf('/') : _assemblyPath.LastIndexOf('\\')) : _assemblyPath;
        }
        /// <summary>
        /// 获取实现指定类型的所有Type
        /// </summary>
        public  IEnumerable<Type> TypesOf(Type type)
        {
            return from t in Types
                             where (type.IsAssignableFrom(t) && !t.IsInterface) || t.GetInterfaces().Any(i => i.Name == type.Name)
                             select t;
        }
    }
}
