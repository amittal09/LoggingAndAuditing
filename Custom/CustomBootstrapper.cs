using Castle.Core.Logging;
using Custom.Dependency;
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

        public static CustomBootstrapper Create([CanBeNull] Action<AbpBootstrapperOptions> optionsAction = null)
        {
            return new CustomBootstrapper();
        }
        private CustomBootstrapper([CanBeNull] Action<AbpBootstrapperOptions> optionsAction = null)
        {

            var options = new AbpBootstrapperOptions();
            optionsAction?.Invoke(options);

            IocManager = options.IocManager;

            _logger = NullLogger.Instance;

            if (!options.DisableAllInterceptors)
            {
                AddInterceptorRegistrars();
            }
        }
        private void AddInterceptorRegistrars()
        {
            AuditingInterceptorRegistrar.Initialize(IocManager);
        }
    }
}
