using Castle.Core.Logging;
using Custom.Collections.Extensions;
using Custom.Configuration.Startup;
using Custom.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Custom.Modules
{
    public class CoreModule
    {
        protected internal IIocManager IocManager { get; internal set; }
        protected internal ICoreStartupConfiguration Configuration { get; internal set; }
        public ILogger Logger { get; set; }
        protected CoreModule()
        {
            Logger = NullLogger.Instance;
        }
        public virtual void PreInitialize()
        {

        }
        public virtual void Initialize()
        {

        }
        public virtual void PostInitialize()
        {

        }
        public virtual void Shutdown()
        {

        }
        public virtual Assembly[] GetAdditionalAssemblies()
        {
            return new Assembly[0];
        }
        public static bool IsCoreModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(CoreModule).IsAssignableFrom(type);
        }
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsCoreModule(moduleType))
            {
                throw new CoreInitializationException("This type is not an core module: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.GetTypeInfo().IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetTypeInfo().GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }

        public static List<Type> FindDependedModuleTypesRecursivelyIncludingGivenModule(Type moduleType)
        {
            var list = new List<Type>();
            AddModuleAndDependenciesRecursively(list, moduleType);
            list.AddIfNotContains(typeof(CoreKernelModule));
            return list;
        }

        private static void AddModuleAndDependenciesRecursively(List<Type> modules, Type module)
        {
            if (!IsCoreModule(module))
            {
                throw new CoreInitializationException("This type is not an Core module: " + module.AssemblyQualifiedName);
            }

            if (modules.Contains(module))
            {
                return;
            }

            modules.Add(module);

            var dependedModules = FindDependedModuleTypes(module);
            foreach (var dependedModule in dependedModules)
            {
                AddModuleAndDependenciesRecursively(modules, dependedModule);
            }
        }
    }
}
