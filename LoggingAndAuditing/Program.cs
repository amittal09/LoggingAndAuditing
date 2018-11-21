using Vestas;
using Vestas.Auditing;
using Vestas.Dependency;
using Vestas.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LoggingAndAuditing
{
    public static class Program
    {

        public static void Main()
        {
            // string json = "DOSLD : DKDKDKDKEFDFDEDA}}";
            //var secondLastIndex = json.AllIndexesOf("}");
            //json = json.ReplaceAt(":[", json.IndexOf(':')).ReplaceAt("}]", secondLastIndex[secondLastIndex.Count - 1]);

            //Console.WriteLine(json);
            var obj = VestasBootstrapper.Create(typeof(MyStartupModule), (VestasBootstrapperOptions option) => new VestasBootstrapperOptions { DisableAllInterceptors = false });
            obj.Initialize();
            Console.WriteLine("test");
            var myClass = obj.IocManager.IocContainer.Resolve<MyClass>();
            myClass.MyTestMethod("This should be audited");

            Console.WriteLine();
            myClass.AnotherMethod("This should be audited too");

            Console.WriteLine();
            myClass.MehodShouldNotAudit("This should not be audited");

            Console.WriteLine("Over...");
            Console.ReadKey();
        }
    }

   
    public class MyClass : ITransientDependency
    {

        [Audited]
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
    class MyStartupModule : VestasModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
    public static class JsonTest
    {
        public static string ReplaceAt(this string source, string newstring, int index)
        {
            var sed = source.ToList();
            sed.RemoveAt(index);
            sed.InsertRange(index, newstring.ToCharArray());
            return new string(sed.ToArray());
        }
        public static List<int> AllIndexesOf(this string value, string substring)
        {
            if (String.IsNullOrEmpty(substring))
                throw new ArgumentException("the string to find may not be empty", nameof(substring));
            var indexes = new List<int>();
            for (int index = 0; ; index += substring.Length)
            {
                index = value.ToUpper().IndexOf(substring.ToUpper(), index, StringComparison.Ordinal);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }
    }
}
