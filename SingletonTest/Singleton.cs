using System;
using System.Reflection;
using System.Linq;

namespace MiniIOC.Common
{
    public sealed class Singleton<T>
    {
        private static readonly Lazy<T> _instance;
        public static int count = 0;
        static Singleton()
        {
            Singleton<T> ._instance = new Lazy<T>(() =>
            { 
             ConstructorInfo[] sources= typeof(T).GetConstructors(BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Instance);
            if(sources.Count<ConstructorInfo>()!=1)
                throw new InvalidOperationException(string.Format("Type {0} must have exactly one constructor.", typeof(T)));
            ConstructorInfo info=sources.FirstOrDefault<ConstructorInfo>(e=>!e.GetParameters().Any<ParameterInfo>()&&e.IsPublic);//无参
            if(info==null)
                throw new InvalidOperationException(string.Format("The constructor for {0} must be private and take no parameters.", typeof(T)));
            return (T)info.Invoke(null);
            });
            count++;
        }
        protected Singleton() { }
        public static T Instance
        {
            get
            {
                return Singleton<T>._instance.Value;
            }
        }
    }
}
