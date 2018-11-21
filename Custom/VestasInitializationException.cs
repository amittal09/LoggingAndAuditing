using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Vestas
{
    public class VestasInitializationException :VestasException
    {
        public VestasInitializationException()
        {

        }
        public VestasInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
        public VestasInitializationException(string message)
            : base(message)
        {

        }
        public VestasInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
