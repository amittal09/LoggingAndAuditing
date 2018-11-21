using Vestas.Dependency;
namespace Vestas
{
    public class VestasBootstrapperOptions
    {
        public bool DisableAllInterceptors { get; set; }

        public IIocManager IocManager { get; set; }

        public VestasBootstrapperOptions()
        {
            IocManager = Dependency.IocManager.Instance;
        }
    }
}
