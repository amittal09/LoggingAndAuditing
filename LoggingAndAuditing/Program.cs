using Custom;
using Custom.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingAndAuditing
{
    public static class Program
    {

        public static void Main()
        {
            CustomBootstrapper.Create().Initialize();
            Console.Write("test");
           new MyClass(). MyTest("This should be audited");
        }
        
        
    }

    [Audited]
    public class MyClass
    {
        public  void MyTest(string param)
        {
            Console.WriteLine("My Test");
        }
    }
}
