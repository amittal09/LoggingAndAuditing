using Castle.Windsor;
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
        static IocManager()
        {
            Instance = new IocManager();
        }
    }
}
