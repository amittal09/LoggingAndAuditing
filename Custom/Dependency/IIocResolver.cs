using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Dependency
{
    public interface IIocResolver
    {
        T Resolve<T>();
        T Resolve<T>(Type type);
    }
}
