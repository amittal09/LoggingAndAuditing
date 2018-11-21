using Vestas.Auditing;
using Vestas.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestas.Configuration.Startup
{
    public interface IVestasStartupConfiguration : IDictionaryBasedConfig
    {
        IIocManager IocManager { get; }
        IAuditingConfiguration Auditing { get; }
        IModuleConfigurations Modules { get; }

        void ReplaceService(Type type, Action replaceAction);
        T Get<T>();

    }
}
