using System;

namespace CryptoCurrency.Net.Model.PriceEstimatation
{
    [Serializable]
    public class CoinEstimate
    {
        public CurrencySymbol CurrencySymbol { get; set; }
        public decimal? FiatEstimate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public decimal ChangePercentage24Hour { get; set; }
    }
}
