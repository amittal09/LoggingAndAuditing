using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Custom.Modules
{
    public class CoreModuleInfo
    {
        public Assembly Assembly { get; }
        public Type Type { get; }
        public CoreModule Instance { get; }
        public bool IsLoadedAsPlugIn { get; }
        public List<CoreModuleInfo> Dependencies { get; }
        public CoreModuleInfo([NotNull] Type type, [NotNull] CoreModule instance, bool isLoadedAsPlugIn)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(instance, nameof(instance));

            Type = type;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugIn;
            Assembly = Type.GetTypeInfo().Assembly;

            Dependencies = new List<CoreModuleInfo>();
        }

        public override string ToString()
        {
            return Type.AssemblyQualifiedName ??
                   Type.FullName;
        }
    }
}
