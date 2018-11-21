using Vestas.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestas.Modules
{
    public  abstract class VestasInitialization
    {
        protected internal IIocManager IocManager { get; internal set; }
    }
}
