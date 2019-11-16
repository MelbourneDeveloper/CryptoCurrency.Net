using System;
using System.Runtime.Serialization;

namespace CryptoCurrency.Net.APIClients
{
    [Serializable]
    internal class GetHoldingsException : Exception
    {
        public GetHoldingsException()
        {
        }

        public GetHoldingsException(string message) : base(message)
        {
        }

        public GetHoldingsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GetHoldingsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}