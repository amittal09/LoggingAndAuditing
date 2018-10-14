using Castle.Core;
using Custom.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Auditing
{
    public class AuditingInterceptorRegistrar
    {
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += (key, handler) =>
            {
                if (!iocManager.IsRegistered<IAuditingConfiguration>())
                {
                    return;
                }

                var auditingConfiguration = iocManager.Resolve<IAuditingConfiguration>();

                if (ShouldIntercept(auditingConfiguration, handler.ComponentModel.Implementation))
                {
                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(AuditingInterceptor)));
                }
            };
        }

        private static bool ShouldIntercept(IAuditingConfiguration auditingConfiguration, Type type)
        {
            
            if (type.GetTypeInfo().IsDefined(typeof(AuditedAttribute), true))
            {
                return true;
            }

            if (type.GetMethods().Any(m => m.IsDefined(typeof(AuditedAttribute), true)))
            {
                return true;
            }

            return false;
        }
    }
}
