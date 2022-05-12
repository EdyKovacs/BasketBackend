using System;
using System.Runtime.Serialization;

namespace basketbackend.Service.Exceptions
{
    public class GracefulException : ApplicationException
    {
        public GracefulException()
        {
        }

        public GracefulException(string message) : base(message)
        {
        }

        public GracefulException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GracefulException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
