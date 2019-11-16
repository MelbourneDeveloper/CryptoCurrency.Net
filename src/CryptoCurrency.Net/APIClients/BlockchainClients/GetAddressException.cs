using System;
using System.Runtime.Serialization;

namespace CryptoCurrency.Net.APIClients
{
    [Serializable]
    public class GetAddressException : Exception
    {
        public GetAddressException()
        {
        }

        public GetAddressException(string message) : base(message)
        {
        }

        public GetAddressException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GetAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}