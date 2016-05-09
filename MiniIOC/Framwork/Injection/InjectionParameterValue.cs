using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MiniIOC.Framwork.Injection
{
    /// <summary>
    /// 参数抽象类
    /// </summary>
    public abstract class InjectionParameterValue
    {
       protected static Dictionary<ConstructorInfo, ParameterInfo[]> paramsList = new Dictionary<ConstructorInfo, ParameterInfo[]>(); //参数列表
       public abstract void SetParams(ConstructorInfo ctor);
       public abstract IEnumerable<ParameterInfo> GetParams(ConstructorInfo ctor);
    }
}
