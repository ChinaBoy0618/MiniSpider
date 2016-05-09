using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MiniIOC.Framwork.Dependency;

namespace MiniIOC.Framwork.Injection
{
    public class InjectionConstructor:InjectionMember
    {
        public InjectionConstructor(Type createType)
        {
            this.DeclaringType = createType;
        }

        public IEnumerable<ConstructorInfo> GetConstructor()
        {
            return from c in this.DeclaringType.GetConstructors()
                    where c.GetCustomAttributes(typeof(DependencyAttribute),false).Length>0
                    select c;
        }
    }
}
