using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniIOC.Framwork.Dependency
{
    public abstract class DependencyResolutionAttribute:Attribute
    {
        public abstract T CreateResolver<T>(Type typeToResolve); 
    }
}
