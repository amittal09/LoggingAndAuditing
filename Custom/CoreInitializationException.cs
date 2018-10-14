using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class CoreInitializationException :CoreException
    {
        public CoreInitializationException()
        {

        }
        public CoreInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
        public CoreInitializationException(string message)
            : base(message)
        {

        }
        public CoreInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
