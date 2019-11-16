using System;
using System.Runtime.Serialization;

namespace CryptoCurrency.Net.AddressManagement
{
    [Serializable]
    public class AddressPathException : Exception
    {
        public AddressPathException()
        {
        }

        public AddressPathException(string message) : base(message)
        {
        }

        public AddressPathException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddressPathException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}