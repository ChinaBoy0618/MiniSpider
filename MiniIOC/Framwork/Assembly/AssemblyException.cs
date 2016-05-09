using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniIOC.Framwork.AssemblyLoader
{
    public class AssemblyException:Exception
    {
        public string AssemblyPath { get; set; }
        public AssemblyException():base()
        { 
        
        }
        public AssemblyException(string path)
            : base()
        {
            AssemblyPath = path;
        }
    }
}
