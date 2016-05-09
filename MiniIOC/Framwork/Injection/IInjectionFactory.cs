using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniIOC.Framwork.Injection
{
    public interface IInjectionFactory
    {
        void Regiter<T1, T2>();
        T Resolve<T>();
        T Resolve<T>(Type type);
        T Resolve<T>(string className);
    }
}
