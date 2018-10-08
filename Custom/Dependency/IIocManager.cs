using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Dependency
{
    public interface IIocManager:IIocRegistrar, IIocResolver, IDisposable
    {
        IWindsorContainer IocContainer { get; }
        bool IsRegistered(Type type);

        bool IsRegistered<T>();
    }
}
