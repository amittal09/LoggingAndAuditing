using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Custom;
using Custom.Auditing;
using Custom.Dependency;
using Custom.Modules;
using System;
using System.Reflection;

namespace LoggingAndAuditing
{
    public static class Program
    {

        public static void Main()
        {
            var obj = CustomBootstrapper.Create(typeof(MyStartupModule), (CustomBootstrapperOptions option) => new CustomBootstrapperOptions { DisableAllInterceptors = false });
            obj.Initialize();
            Console.WriteLine("test");
            obj.IocManager.IocContainer.Resolve<MyClass>().MyTestMethod("This should be audited");
            Console.WriteLine("Over...");
            Console.ReadKey();
        }
    }

    [Audited]
    public class MyClass : ITransientDependency
    {
        [Audited]
        public void MyTestMethod(string param)
        {
            Console.WriteLine(param);
        }
    }
    class MyStartupModule : CoreModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
