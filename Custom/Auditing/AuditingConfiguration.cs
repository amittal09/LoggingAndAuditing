using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Auditing
{
    public class AuditingConfiguration : IAuditingConfiguration
    {
        public bool IsEnabled { get; set; }
        public List<Type> IgnoredTypes { get; }

        public AuditingConfiguration()
        {
            IsEnabled = true;
            IgnoredTypes = new List<Type>();
        }
    }
}
