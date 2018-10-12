using Custom.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Modules
{
    public  abstract class CustomInitialization
    {
        protected internal IIocManager IocManager { get; internal set; }
    }
}
