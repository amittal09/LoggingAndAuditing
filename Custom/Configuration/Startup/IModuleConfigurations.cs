using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Configuration.Startup
{
    public interface IModuleConfigurations
    {
        ICoreStartupConfiguration CoreConfiguration { get; }

    }
}
