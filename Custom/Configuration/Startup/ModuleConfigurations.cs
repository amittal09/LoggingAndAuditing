namespace Vestas.Configuration.Startup
{
    internal class ModuleConfigurations : IModuleConfigurations
    {
        public IVestasStartupConfiguration CoreConfiguration { get; private set; }

        public ModuleConfigurations(IVestasStartupConfiguration coreConfiguration)
        {
            CoreConfiguration = coreConfiguration;
        }
    }
}
