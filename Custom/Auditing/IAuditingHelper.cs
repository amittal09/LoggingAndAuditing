using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Auditing
{
    public interface IAuditingHelper
    {
        bool ShouldSaveAudit(MethodInfo methodInfo, bool defaultValue = false);

        AuditInfo CreateAuditInfo(Type type, MethodInfo method, object[] arguments);

        AuditInfo CreateAuditInfo(Type type, MethodInfo method, IDictionary<string, object> arguments);

        void Save(AuditInfo auditInfo);

        Task SaveAsync(AuditInfo auditInfo);
    }
}
