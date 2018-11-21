using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Vestas.Auditing;
using Vestas.Configuration.Startup;
using Vestas.Reflection;
using Vestas.Modules;

namespace Vestas.Dependency.Installers
{
    public class VestasComponentInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
            Component.For<IAuditingConfiguration, AuditingConfiguration>().ImplementedBy<AuditingConfiguration>().LifestyleSingleton(),
            Component.For<IModuleConfigurations, ModuleConfigurations>().ImplementedBy<ModuleConfigurations>().LifestyleSingleton(),
            Component.For<IVestasStartupConfiguration, VestasStartupConfiguration>().ImplementedBy<VestasStartupConfiguration>().LifestyleSingleton(),
            Component.For<ITypeFinder, TypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
            Component.For<IVestasModuleManager, VestasModuleManager>().ImplementedBy<VestasModuleManager>().LifestyleSingleton(),
            Component.For<IAssemblyFinder, VestasAssemblyFinder>().ImplementedBy<VestasAssemblyFinder>().LifestyleSingleton()
           );
        }
    }
}
