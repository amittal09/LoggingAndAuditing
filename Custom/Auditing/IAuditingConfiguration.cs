using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestas.Auditing
{
    public interface IAuditingConfiguration
    {
        bool IsEnabled { get; set; }
        List<Type> IgnoredTypes { get; }

    }
}
