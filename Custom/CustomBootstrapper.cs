using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Custom.Auditing;
using Custom.Dependency;
using Custom.Dependency.Installers;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class CustomBootstrapper
    {
        public IIocManager IocManager { get; }

        private ILogger _logger;
        protected bool IsDisposed;

        public static CustomBootstrapper Create([CanBeNull] Action<CustomBootstrapperOptions> optionsAction = null)
        {
            return new CustomBootstrapper();
        }
        private CustomBootstrapper([CanBeNull] Action<CustomBootstrapperOptions> optionsAction = null)
        {

            var options = new CustomBootstrapperOptions();
            optionsAction?.Invoke(options);

            IocManager = options.IocManager;

            _logger = NullLogger.Instance;

            if (!options.DisableAllInterceptors)
            {
                AddInterceptorRegistrars();
            }
        }

        public virtual void Initialize()
        {

            try
            {
                RegisterBootstrapper();
                IocManager.IocContainer.Install(new CustomComponentInstaller());


            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.ToString(), ex);
                throw;
            }
        }
        private void AddInterceptorRegistrars()
        {
            AuditingInterceptorRegistrar.Initialize(IocManager);
        }
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;
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

    }
}
