using System;

namespace CryptoCurrency.Net.Model.PriceEstimatation
{
    [Serializable]
    public class EstimatedPricesModel
    {
        public CoinEstimateList Result { get; } = new CoinEstimateList();
        public DateTime? LastUpdate { get; set; }
    }
}
