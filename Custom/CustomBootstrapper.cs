using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Custom.Auditing;
using Custom.Configuration.Startup;
using Custom.Dependency;
using Custom.Dependency.Installers;
using Custom.Modules;
using JetBrains.Annotations;
using System;
using System.Reflection;

namespace Custom
{
    public class CustomBootstrapper : IDisposable
    {
        public Type StartupModule { get; }
        public IIocManager IocManager { get; }
        protected bool IsDisposed;

        private CoreModuleManager _moduleManager;
        private ILogger _logger;

        private CustomBootstrapper([NotNull] Type startupModule, [CanBeNull] Action<CustomBootstrapperOptions> optionsAction = null)
        {
            Check.NotNull(startupModule, nameof(startupModule));

            var options = new CustomBootstrapperOptions();
            optionsAction?.Invoke(options);

            if (!typeof(CoreModule).GetTypeInfo().IsAssignableFrom(startupModule))
            {
                throw new ArgumentException($"{nameof(startupModule)} should be derived from {nameof(CoreModule)}.");
            }

            StartupModule = startupModule;

            IocManager = options.IocManager;

            _logger = NullLogger.Instance;

            if (!options.DisableAllInterceptors)
            {
                AddInterceptorRegistrars();
            }
        }

        public static CustomBootstrapper Create<TStartupModule>([CanBeNull] Action<CustomBootstrapperOptions> optionsAction = null)
            where TStartupModule : CoreModule
        {
            return new CustomBootstrapper(typeof(TStartupModule), optionsAction);
        }

        public static CustomBootstrapper Create([NotNull] Type startupModule, [CanBeNull] Action<CustomBootstrapperOptions> optionsAction = null)
        {
            return new CustomBootstrapper(startupModule, optionsAction);
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
                IocManager.IocContainer.Install(new CustomComponentInstaller());

                IocManager.Resolve<CoreStartupConfiguration>().Initialize();

                _moduleManager = IocManager.Resolve<CoreModuleManager>();
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
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(CustomBootstrapper));
            }
        }

        private void RegisterBootstrapper()
        {
            if (!IocManager.IsRegistered<CustomBootstrapper>())
            {
                IocManager.IocContainer.Register(
                    Component.For<CustomBootstrapper>().Instance(this)
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
