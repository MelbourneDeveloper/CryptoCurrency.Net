using System;
using System.Runtime.Serialization;

namespace CryptoCurrency.Net.AddressManagement
{
    [Serializable]
    public class ParseAddressPathException : Exception
    {
        public ParseAddressPathException()
        {
        }

        public ParseAddressPathException(string message) : base(message)
        {
        }

        public ParseAddressPathException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ParseAddressPathException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}