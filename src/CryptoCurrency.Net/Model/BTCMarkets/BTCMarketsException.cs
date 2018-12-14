using System;

namespace CryptoCurrency.Net.Model.BTCMarkets
{
    public class BTCMarketsException : Exception
    {
        public ErrorResult ErrorResult { get; }

        public BTCMarketsException(ErrorResult errorResult) : base(errorResult.errorMessage)
        {
            ErrorResult = errorResult;
        }
    }
}
