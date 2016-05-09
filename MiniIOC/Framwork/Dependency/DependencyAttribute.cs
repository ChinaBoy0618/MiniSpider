using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniIOC.Framwork.Dependency
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method|AttributeTargets.Interface|AttributeTargets.Property|AttributeTargets.Constructor)]
    public class DependencyAttribute : DependencyResolutionAttribute
    {
        private readonly string name;

        public DependencyAttribute()
            : this(null)
        {
        }

        public DependencyAttribute(string name)
        {
            this.name = name;
        }
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public override T CreateResolver<T>(Type typeToResolve)
        {
            throw new NotImplementedException();
        }
    }
}
