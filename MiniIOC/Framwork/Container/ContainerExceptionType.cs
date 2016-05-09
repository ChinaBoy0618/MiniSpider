using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MiniIOC.Framwork
{
    /// <summary>
    /// 容器异常类型
    /// </summary>
    public enum ContainerExceptionType
    {
        [Description("实体未注册")]
        NullEntityInContainer
    }
}
