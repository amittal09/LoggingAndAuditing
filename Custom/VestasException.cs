using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Vestas
{
    public class VestasException :Exception
    {
        public VestasException()
        {

        }
        public VestasException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
        public VestasException(string message)
            : base(message)
        {

        }
        public VestasException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
