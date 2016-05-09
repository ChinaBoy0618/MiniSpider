using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniIOC.Framwork
{
    /// <summary>
    /// 容器接口
    /// </summary>
    public interface IMiniContainer
    {
        void Regiter<T1, T2>();
        T Resolve<T>();
        T Resolve<T>(Type type);
        T Resolve<T>(string className);
    }
}
