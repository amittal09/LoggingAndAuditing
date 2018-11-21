using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Vestas.Auditing;
using Vestas.Configuration.Startup;
using Vestas.Dependency;
using Vestas.Dependency.Installers;
using Vestas.Modules;
using JetBrains.Annotations;
using System;
using System.Reflection;

namespace Vestas
{
    public class VestasBootstrapper : IDisposable
    {
        public Type StartupModule { get; }
        public IIocManager IocManager { get; }
        protected bool IsDisposed;

        private VestasModuleManager _moduleManager;
        private ILogger _logger;

        private VestasBootstrapper([NotNull] Type startupModule, [CanBeNull] Action<VestasBootstrapperOptions> optionsAction = null)
        {
            Check.NotNull(startupModule, nameof(startupModule));

            var options = new VestasBootstrapperOptions();
            optionsAction?.Invoke(options);

            if (!typeof(VestasModule).GetTypeInfo().IsAssignableFrom(startupModule))
            {
                throw new ArgumentException($"{nameof(startupModule)} should be derived from {nameof(VestasModule)}.");
            }

            StartupModule = startupModule;

            IocManager = options.IocManager;

            _logger = NullLogger.Instance;

            if (!options.DisableAllInterceptors)
            {
                AddInterceptorRegistrars();
            }
        }

        public static VestasBootstrapper Create<TStartupModule>([CanBeNull] Action<VestasBootstrapperOptions> optionsAction = null)
            where TStartupModule : VestasModule
        {
            return new VestasBootstrapper(typeof(TStartupModule), optionsAction);
        }

        public static VestasBootstrapper Create([NotNull] Type startupModule, [CanBeNull] Action<VestasBootstrapperOptions> optionsAction = null)
        {
            return new VestasBootstrapper(startupModule, optionsAction);
        }

        private void AddInterceptorRegistrars()
        {
            AuditingInterceptorRegistrar.Initialize(IocManager);
        }

        public virtual void Initialize()
        {
            ResolveLogger();

            try
            {
                RegisterBootstrapper();
                IocManager.IocContainer.Install(new VestasComponentInstaller());

                IocManager.Resolve<VestasStartupConfiguration>().Initialize();

                _moduleManager = IocManager.Resolve<VestasModuleManager>();

                _moduleManager.Initialize(StartupModule);
                _moduleManager.StartModules();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.ToString(), ex);
                throw;
            }
        }

        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
            {
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(VestasBootstrapper));
            }
        }

        private void RegisterBootstrapper()
        {
            if (!IocManager.IsRegistered<VestasBootstrapper>())
            {
                IocManager.IocContainer.Register(
                    Component.For<VestasBootstrapper>().Instance(this)
                    );
            }
        }

        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            _moduleManager?.ShutdownModules();
        }
    }
}
