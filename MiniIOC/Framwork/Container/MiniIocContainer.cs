using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using MiniIOC.Framwork.Common;
using MiniIOC.Framwork.Injection;

namespace MiniIOC.Framwork
{
    public class MiniIocContainer:IMiniContainer
    {
        /// <summary>
        /// 获取容器实例
        /// </summary>
        public static IMiniContainer Instance
        {
            get
            {
                return Singleton< MiniIocContainer>.Instance;
            }
        }
        IInjectionFactory injectionFactory = Singleton<InjectionFactory>.Instance;
        public MiniIocContainer()
        { 
            
        }
        //public MiniIocContainer(string assemblyPath)
        //{

        //}
        /// <summary>
        /// 容器注册器
        /// </summary>
        public virtual void Regiter<T1,T2>() 
        {
            injectionFactory.Regiter<T1, T2>();
        }
        public T Resolve<T>()
        {
            return injectionFactory.Resolve<T>();
        }
        public T Resolve<T>(Type type)
        {
            return injectionFactory.Resolve<T>(type);
        }
         public T Resolve<T>(string className)
        {
            return injectionFactory.Resolve<T>(className);
        }
    }
}
