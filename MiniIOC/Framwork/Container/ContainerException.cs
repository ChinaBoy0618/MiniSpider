using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniIOC.Framwork
{
    public class ContainerException:Exception
    {
        public ContainerExceptionType type { get; set; }
        public ContainerException()
            : base()
        { }
        public ContainerException(string message,ContainerExceptionType type)
            : base(message)
        {
            this.type = type;
        }

    }
}
