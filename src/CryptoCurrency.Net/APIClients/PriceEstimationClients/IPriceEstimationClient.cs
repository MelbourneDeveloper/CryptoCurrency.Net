using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.PriceEstimatation;

namespace CryptoCurrency.Net.APIClients.PriceEstimationClients
{
    public interface IPriceEstimationClient
    {
        Task<EstimatedPricesModel> GetPrices(IEnumerable<CurrencySymbol> currencies, string fiatCurrency);
        decimal SuccessRate { get; }
        TimeSpan AverageCallTimespan { get; }
    }
}
