using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Custom.Auditing;
using Custom.Configuration.Startup;
using Custom.Reflection;
using Custom.Modules;

namespace Custom.Dependency.Installers
{
    public class CustomComponentInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
            Component.For<IAuditingConfiguration, AuditingConfiguration>().ImplementedBy<AuditingConfiguration>().LifestyleSingleton(),
            Component.For<IModuleConfigurations, ModuleConfigurations>().ImplementedBy<ModuleConfigurations>().LifestyleSingleton(),
            Component.For<ICoreStartupConfiguration, CoreStartupConfiguration>().ImplementedBy<CoreStartupConfiguration>().LifestyleSingleton(),
            Component.For<ITypeFinder, TypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
            Component.For<ICoreModuleManager, CoreModuleManager>().ImplementedBy<CoreModuleManager>().LifestyleSingleton(),
            Component.For<IAssemblyFinder, CoreAssemblyFinder>().ImplementedBy<CoreAssemblyFinder>().LifestyleSingleton()
           );
        }
    }
}
