using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoCurrency.Net.Base.Model;
using CryptoCurrency.Net.Base.Model.PriceEstimatation;

namespace CryptoCurrency.Net.Base.Abstractions.APIClients
{
    public interface IPriceEstimationClient
    {
        Task<EstimatedPricesModel> GetPrices(IEnumerable<CurrencySymbol> currencies, string fiatCurrency);
        decimal SuccessRate { get; }
        TimeSpan AverageCallTimespan { get; }
    }
}
