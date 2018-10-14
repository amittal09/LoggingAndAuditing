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
            var obj = CustomBootstrapper.Create(typeof(MyStartupModule));
            obj.Initialize();
            obj.IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());
            obj.IocManager.IocContainer.Register(Component.For<IAuditSerializer>().ImplementedBy<JsonNetAuditSerializer>());
            obj.IocManager.IocContainer.Register(Component.For<IAuditingHelper>().ImplementedBy<AuditingHelper>());
            obj.IocManager.IocContainer.Register(Component.For<IInterceptor>().ImplementedBy<AuditingInterceptor>());
            obj.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Console.WriteLine("test");
            obj.IocManager.IocContainer.Resolve<MyClass>().Dfsfdffsaffsdf("This should be audited");
        }
    }

    [Audited]
    public class MyClass : ITransientDependency
    {
        public void Dfsfdffsaffsdf(string param)
        {
            Console.WriteLine(param);
        }
    }
    class MyStartupModule : CoreModule
    {

    }
}
