using System;
using System.Collections.Generic;

namespace Vestas.Modules
{
    public interface IVestasModuleManager
    {
        VestasModuleInfo StartupModule { get; }

        IReadOnlyList<VestasModuleInfo> Modules { get; }
        void Initialize(Type startupModule);

        void StartModules();

        void ShutdownModules();
    }
}
