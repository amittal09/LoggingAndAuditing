using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Vestas.Modules
{
    public class VestasModuleInfo
    {
        public Assembly Assembly { get; }
        public Type Type { get; }
        public VestasModule Instance { get; }
        public bool IsLoadedAsPlugIn { get; }
        public List<VestasModuleInfo> Dependencies { get; }
        public VestasModuleInfo([NotNull] Type type, [NotNull] VestasModule instance, bool isLoadedAsPlugIn)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(instance, nameof(instance));

            Type = type;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugIn;
            Assembly = Type.GetTypeInfo().Assembly;

            Dependencies = new List<VestasModuleInfo>();
        }

        public override string ToString()
        {
            return Type.AssemblyQualifiedName ??
                   Type.FullName;
        }
    }
}
