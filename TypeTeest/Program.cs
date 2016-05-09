using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TypeTeest
{
    class Program
    {
        static void Main(string[] args)
        {

            Type type = Type.GetType("MiniIOC.ConfigHelper,SpliderFramework");
            ConstructorInfo[] source = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (type != null && type.GetMethod("GetAppSettings") != null && source.Length>0)
            {
                
                MethodInfo method = type.GetMethod("GetAppSettings");
                Console.WriteLine(method.Invoke(source[0].Invoke(null), new object[] { "test" }));
            }

         }
      
    }
}
