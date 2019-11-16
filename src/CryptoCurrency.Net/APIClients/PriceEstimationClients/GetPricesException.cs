using System;
using System.Runtime.Serialization;

namespace CryptoCurrency.Net.APIClients.PriceEstimationClients
{
    [Serializable]
    public class GetPricesException : Exception
    {
        public GetPricesException()
        {
        }

        public GetPricesException(string message) : base(message)
        {
        }

        public GetPricesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GetPricesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}