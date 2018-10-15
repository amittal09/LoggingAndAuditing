namespace Custom.Configuration.Startup
{
    internal class ModuleConfigurations : IModuleConfigurations
    {
        public ICoreStartupConfiguration CoreConfiguration { get; private set; }

        public ModuleConfigurations(ICoreStartupConfiguration coreConfiguration)
        {
            CoreConfiguration = coreConfiguration;
        }
    }
}
