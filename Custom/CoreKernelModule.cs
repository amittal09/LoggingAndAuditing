using Custom.Collections.Extensions;
using Custom.Configuration.Startup;
using Custom.Dependency;
using Custom.Modules;
using Custom.Reflection.Extensions;
using System.IO;
using System.Linq.Expressions;

namespace Custom
{
    public sealed class CoreKernelModule :CoreModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());


            AddIgnoredTypes();
        }
        public override void Initialize()
        {
            foreach (var replaceAction in ((CoreStartupConfiguration)Configuration).ServiceReplaceActions.Values)
            {
                replaceAction();
            }



            IocManager.RegisterAssemblyByConvention(typeof(CoreKernelModule).GetAssembly(),
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
