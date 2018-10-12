using Castle.MicroKernel.Registration;
using Custom;
using Custom.Auditing;
using Custom.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LoggingAndAuditing
{
    public static class Program
    {

        public static void Main()
        {
            var obj = CustomBootstrapper.Create();
            obj.Initialize();
            obj.IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());
            obj.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Console.Write("test");
            obj.IocManager.IocContainer.Resolve<MyClass>().MyTest("This should be audited");
        }


    }

    [Audited]
    public class MyClass : ITransientDependency
    {
        public  void MyTest(string param)
        {
            Console.WriteLine("My Test");
        }
    }
    
}
