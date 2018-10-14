using Custom.Auditing;
using Custom.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Configuration.Startup
{
    public class CoreStartupConfiguration : DictionaryBasedConfig, ICoreStartupConfiguration
    {
        public IIocManager IocManager { get; }
        public IAuditingConfiguration Auditing { get; private set; }
        public IModuleConfigurations Modules { get; private set; }

        public Dictionary<Type, Action> ServiceReplaceActions { get; private set; }
        public CoreStartupConfiguration(IIocManager iocManager)
        {
            IocManager = iocManager;
        }
        public void Initialize()
        {
            Modules = IocManager.Resolve<IModuleConfigurations>();
            Auditing = IocManager.Resolve<IAuditingConfiguration>();
            ServiceReplaceActions = new Dictionary<Type, Action>();

        }
        public void ReplaceService(Type type, Action replaceAction)
        {
            ServiceReplaceActions[type] = replaceAction;
        }

        public T Get<T>()
        {
            return GetOrCreate(typeof(T).FullName, () => IocManager.Resolve<T>());
        }
        
    }
}
