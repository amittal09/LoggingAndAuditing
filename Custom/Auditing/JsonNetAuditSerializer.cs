using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Auditing
{
    public class JsonNetAuditSerializer : IAuditSerializer
    {
        private readonly IAuditingConfiguration _configuration;

        public JsonNetAuditSerializer(IAuditingConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Serialize(object obj)
        {
            var options = new JsonSerializerSettings
            {
                ContractResolver = new AuditingContractResolver(_configuration.IgnoredTypes)
            };

            return JsonConvert.SerializeObject(obj, options);
        }
    }
}
