using System.Collections.Generic;

namespace CryptoCurrency.Net.Model
{
    public class GetHoldingsResult : APIResultBase
    {
        public List<CurrencyHolding> Result { get; } = new List<CurrencyHolding>();

        public GetHoldingsResult(object tag) : base(tag)
        {
        }
    }
}
