using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MiniIOC.Framwork.Injection
{
    public class InjectionConstructorParameterValue:InjectionParameterValue
    {
        public override void SetParams(System.Reflection.ConstructorInfo ctor)
        {
            if (ctor==null||paramsList.ContainsKey(ctor))
                return;
            paramsList.Add(ctor, ctor.GetParameters());
        }

        public override IEnumerable<ParameterInfo> GetParams(System.Reflection.ConstructorInfo ctor)
        {
            if (ctor == null)
                return null;
            if (!paramsList.ContainsKey(ctor))
                paramsList.Add(ctor, ctor.GetParameters());
            return paramsList[ctor];
        }

      
    }
}
