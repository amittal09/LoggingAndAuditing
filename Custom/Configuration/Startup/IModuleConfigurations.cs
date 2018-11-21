using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestas.Configuration.Startup
{
    public interface IModuleConfigurations
    {
        IVestasStartupConfiguration CoreConfiguration { get; }

    }
}
