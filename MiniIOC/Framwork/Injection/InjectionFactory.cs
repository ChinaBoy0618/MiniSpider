using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MiniIOC.Framwork.Config;
using MiniIOC.Framwork.Dependency;
using MiniIOC.Framwork.Common;

namespace MiniIOC.Framwork.Injection
{
    public class InjectionFactory : IInjectionFactory
    {
        private static Dictionary<Type, Type> infoList = new Dictionary<Type, Type>();
        private InjectionParameterValue paramsValue = Singleton<InjectionConstructorParameterValue>.Instance;
        private readonly TypeResolverImpl ResolverImpl = Singleton<TypeResolverImpl>.Instance;
        public T CreateInstace<T>(Type createType) where T : new()
        {
            return default(T);

        }
        /// <summary>
        /// 容器注册器
        /// </summary>
        public virtual void Regiter<T1, T2>()
        {
            if (infoList.ContainsKey(typeof(T1)))
                infoList[typeof(T1)] = typeof(T2);
            else
                infoList.Add(typeof(T1), typeof(T2));
        }
        /// <summary>
        /// 控制反转
        /// </summary>
        public virtual Type Resolve(Type type)
        {
            if (infoList.ContainsKey(type))
                return infoList[type];
            else
                //throw new ContainerException("实体未注册", ContainerExceptionType.NullEntityInContainer);
                return null;
        }
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public T Resolve<T>()
        {
            var type = ResolverImpl.ResolveType(typeof(T)) ?? Resolve(typeof(T)) ?? typeof(T);
            T instance = CreateInstanceByCtor<T>(type);
            return Dependency<T>(instance);
        }
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public T Resolve<T>(Type type)
        {
            T instance = default(T);
            if (type != null)
                instance = CreateInstance<T>(type);
            return Dependency<T>(instance);
        }
        public T Resolve<T>(string className)
        {
            return CreateInstance<T>(ResolverImpl.ResolveType(className));
        }
        private T CreateInstance<T>(Type type) 
        {
            ConstructorInfo[] sources = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (sources.Count<ConstructorInfo>() != 1)
                throw new InvalidOperationException(string.Format("Type {0} must have exactly one constructor.", type));
            ConstructorInfo info = sources.FirstOrDefault<ConstructorInfo>(e => !e.GetParameters().Any<ParameterInfo>() && e.IsPublic);//无参
            if (info == null)
                throw new InvalidOperationException(string.Format("The constructor for {0} must be public and take parameters.", type));

            return (T)info.Invoke(null);
        }
        private T Dependency<T>(T instance)
        {
            //依赖注入
            var type = typeof(T);
            IEnumerable<MemberInfo> memberInfos = from m in type.GetMembers(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                                                  where m.GetCustomAttributes(typeof(DependencyAttribute), false).Length > 0&&m.MemberType!=MemberTypes.Constructor
                                                  select m;
            foreach (MemberInfo m in memberInfos)
            {
                type.GetField(m.Name).SetValue(instance, CreateInstance<T>(ResolverImpl.ResolveType(m.DeclaringType.FullName) ?? Resolve(m.DeclaringType)));
            }
            return instance;
        }
        private T CreateInstanceByCtor<T>(Type createType)
        {
            IEnumerable<ConstructorInfo> ctors = new InjectionConstructor(createType).GetConstructor();
            Guard.ArgumentNotNull(ctors,"IEnumerable<ConstructorInfo>");

            //获取构造参数，注入
            ConstructorInfo ctor = ctors.FirstOrDefault(c => c.GetParameters().Any<ParameterInfo>() && c.IsPublic);
            List<object> list = null;
            if (ctor != null)
            {
                IEnumerable<ParameterInfo> paramInfos = paramsValue.GetParams(ctor);
                list = new List<object>(paramInfos.Count());
                foreach (var info in paramInfos)
                {
                    list.Add(CreateInstance<object>(ResolverImpl.ResolveType(info.ParameterType) ?? Resolve(info.ParameterType)));
                }
                return (T)ctor.Invoke(list.ToArray());
            }
            else
            {
                return  CreateInstance<T>(createType);
            }
        }
    }
}
