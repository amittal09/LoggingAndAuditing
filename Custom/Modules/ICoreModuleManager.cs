using System;
using System.Collections.Generic;

namespace Custom.Modules
{
    public interface ICoreModuleManager
    {
        CoreModuleInfo StartupModule { get; }

        IReadOnlyList<CoreModuleInfo> Modules { get; }
        void Initialize(Type startupModule);

        void StartModules();

        void ShutdownModules();
    }
}
