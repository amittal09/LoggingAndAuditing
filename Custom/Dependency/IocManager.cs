using Castle.DynamicProxy;
using Castle.Windsor;
using Castle.Windsor.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Dependency
{
    public class IocManager :IIocManager
    {
        public static IocManager Instance { get; private set; }

        public IWindsorContainer IocContainer { get; private set; }
        private static readonly ProxyGenerator ProxyGeneratorInstance = new ProxyGenerator();
        static IocManager()
        {
            Instance = new IocManager();
        }
        public IocManager()
        {
            IocContainer = new WindsorContainer(new DefaultProxyFactory(ProxyGeneratorInstance));

        }
        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

        public bool IsRegistered<TType>()
        {
            return IocContainer.Kernel.HasComponent(typeof(TType));
        }

        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        public T Resolve<T>(Type type)
        {
            return (T)IocContainer.Resolve(type);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
