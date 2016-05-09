using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniIOC.Framwork.Injection
{
    /// <summary>
    /// 依赖注入成员
    /// </summary>
    public abstract class InjectionMember
    {
        public Type DeclaringType;
        //public abstract bool Matches(Type createType,);
    }
}
