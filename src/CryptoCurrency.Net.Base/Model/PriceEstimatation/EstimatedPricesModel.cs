using System;

namespace CryptoCurrency.Net.Base.Model.PriceEstimatation
{
    [Serializable]
    public class EstimatedPricesModel
    {
        public CoinEstimateCollection Result { get; } = new CoinEstimateCollection();
        public DateTime? LastUpdate { get; set; }
    }
}
