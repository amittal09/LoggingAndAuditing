using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class CoreException :Exception
    {
        public CoreException()
        {

        }
        public CoreException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
        public CoreException(string message)
            : base(message)
        {

        }
        public CoreException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
