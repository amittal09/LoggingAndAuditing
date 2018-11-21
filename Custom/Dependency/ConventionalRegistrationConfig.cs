using Vestas.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestas.Dependency
{
    public class ConventionalRegistrationConfig : DictionaryBasedConfig
    {
        public bool InstallInstallers { get; set; }

        public ConventionalRegistrationConfig()
        {
            InstallInstallers = true;
        }
    }
}
