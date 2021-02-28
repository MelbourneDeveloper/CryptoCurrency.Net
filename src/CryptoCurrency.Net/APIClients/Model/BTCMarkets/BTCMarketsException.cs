using System;

namespace CryptoCurrency.Net.APIClients.Model.BTCMarkets
{
    [Serializable]
    public class BTCMarketsException : Exception
    {
        public ErrorResult ErrorResult { get; }

        public BTCMarketsException(ErrorResult errorResult) : base(errorResult.errorMessage) => ErrorResult = errorResult ?? throw new ArgumentNullException(nameof(errorResult));

        public BTCMarketsException()
        {
        }

        public BTCMarketsException(string message) : base(message)
        {
        }

        public BTCMarketsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
