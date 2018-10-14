using Custom.Auditing;
using Custom.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Configuration.Startup
{
    public interface ICoreStartupConfiguration : IDictionaryBasedConfig
    {
        IIocManager IocManager { get; }
        IAuditingConfiguration Auditing { get; }
        IModuleConfigurations Modules { get; }

        void ReplaceService(Type type, Action replaceAction);
        T Get<T>();

    }
}
