using Castle.Core.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Auditing
{
    public class AuditingHelper : IAuditingHelper
    {
        private readonly IAuditingConfiguration _configuration;
        private readonly IAuditSerializer _auditSerializer;

        public AuditingHelper(
            IAuditingConfiguration configuration,
            IAuditSerializer auditSerializer)
        {
            _configuration = configuration;
            _auditSerializer = auditSerializer;

        }
        public bool ShouldSaveAudit(MethodInfo methodInfo, bool defaultValue = false)
        {
            if (!_configuration.IsEnabled)
            {
                return false;
            }
            if (!methodInfo.IsPublic)
            {
                return false;
            }
            if (methodInfo.IsDefined(typeof(AuditedAttribute), true))
            {
                return true;
            }

            if (methodInfo.IsDefined(typeof(DisableAuditingAttribute), true))
            {
                return false;
            }
            var classType = methodInfo.DeclaringType;
            if (classType != null)
            {
                if (classType.GetTypeInfo().IsDefined(typeof(AuditedAttribute), true))
                {
                    return true;
                }

                if (classType.GetTypeInfo().IsDefined(typeof(DisableAuditingAttribute), true))
                {
                    return false;
                }
                
            }

            return defaultValue;
        }

        public AuditInfo CreateAuditInfo(Type type, MethodInfo method, object[] arguments)
        {
            return CreateAuditInfo(type, method, CreateArgumentsDictionary(method, arguments));
        }

        public AuditInfo CreateAuditInfo(Type type, MethodInfo method, IDictionary<string, object> arguments)
        {
            var auditInfo = new AuditInfo
            {
                UserId = 100,
                ServiceName = type != null
                    ? type.FullName
                    : "",
                MethodName = method.Name,
                Parameters = ConvertArgumentsToJson(arguments),
                ExecutionTime = DateTime.Now
            };

            try
            {
                // _auditInfoProvider.Fill(auditInfo);
            }
            catch (Exception ex)
            {
                //  Logger.Warn(ex.ToString(), ex);
            }

            return auditInfo;

        }

        public void Save(AuditInfo auditInfo)
        {
            Console.WriteLine(auditInfo.MethodName);
            Console.WriteLine(auditInfo.Parameters);

        }


        public async Task SaveAsync(AuditInfo auditInfo)
        {
            
        }

        private string ConvertArgumentsToJson(IDictionary<string, object> arguments)
        {
            try
            {
                if (arguments.IsNullOrEmpty())
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();

                foreach (var argument in arguments)
                {
                    if (argument.Value != null && _configuration.IgnoredTypes.Any(t => t.IsInstanceOfType(argument.Value)))
                    {
                        dictionary[argument.Key] = null;
                    }
                    else
                    {
                        dictionary[argument.Key] = argument.Value;
                    }
                }

                return _auditSerializer.Serialize(dictionary);
            }
            catch (Exception ex)
            {
                return "{}";
            }
        }

        private static Dictionary<string, object> CreateArgumentsDictionary(MethodInfo method, object[] arguments)
        {
            var parameters = method.GetParameters();
            var dictionary = new Dictionary<string, object>();

            for (var i = 0; i < parameters.Length; i++)
            {
                dictionary[parameters[i].Name] = arguments[i];
            }

            return dictionary;
        }
    }
}
