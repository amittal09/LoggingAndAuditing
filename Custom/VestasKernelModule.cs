using Vestas.Collections.Extensions;
using Vestas.Configuration.Startup;
using Vestas.Dependency;
using Vestas.Modules;
using Vestas.Reflection.Extensions;
using System.IO;
using System.Linq.Expressions;

namespace Vestas
{
    public sealed class VestasKernelModule :VestasModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());

            AddIgnoredTypes();
        }
        public override void Initialize()
        {
            foreach (var replaceAction in ((VestasStartupConfiguration)Configuration).ServiceReplaceActions.Values)
            {
                replaceAction();
            }



            IocManager.RegisterAssemblyByConvention(typeof(VestasKernelModule).GetAssembly(),
                new ConventionalRegistrationConfig
                {
                    InstallInstallers = false
                });
        }
        public override void PostInitialize()
        {
            RegisterMissingComponents();
            
        }
        public override void Shutdown()
        {
            
        }
        private void AddIgnoredTypes()
        {
            var commonIgnoredTypes = new[]
            {
                typeof(Stream),
                typeof(Expression)
            };

            foreach (var ignoredType in commonIgnoredTypes)
            {
                Configuration.Auditing.IgnoredTypes.AddIfNotContains(ignoredType);
            }
        }
        private void RegisterMissingComponents()
        {
        }
    }
}
