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
           var myClass =  obj.IocManager.IocContainer.Resolve<MyClass>();
            myClass.MyTestMethod("This should be audited");

            Console.WriteLine();
            myClass.AnotherMethod("This should be audited too");
            
            Console.WriteLine();
            myClass.MehodShouldNotAudit("This should not be audited");

            Console.WriteLine("Over...");
            Console.ReadKey();
        }
    }

    [Audited]
    public class MyClass : ITransientDependency
    {
        
        public virtual void MyTestMethod(string param)
        {
            Console.WriteLine(param);
        }
        public virtual void AnotherMethod(string IamMethodParameter)
        {
            Console.WriteLine(IamMethodParameter);
        }

        public virtual void TestException()
        {
            try
            {
                Console.WriteLine("I will throw exception");
                throw new Exception($"Exception from {nameof(TestException)}");
            }
            catch (Exception)
            {

               
            }
        }

        [DisableAuditing]
        public virtual void MehodShouldNotAudit(string param)
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
