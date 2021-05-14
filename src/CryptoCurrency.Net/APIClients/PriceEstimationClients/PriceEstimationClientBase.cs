using CryptoCurrency.Net.Base.Model;
using CryptoCurrency.Net.Base.Model.PriceEstimatation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestClient.Net.Abstractions;
using Microsoft.Extensions.Logging;

namespace CryptoCurrency.Net.APIClients.PriceEstimationClients
{
    public abstract class PriceEstimationClientBase : APIClientBase
    {
        protected PriceEstimationClientBase(Func<Uri, IClient> restClientFactory, ILogger logger) : base(restClientFactory, logger)
        {
        }

        public async Task<EstimatedPricesModel> GetPrices(IEnumerable<CurrencySymbol> currencies, string fiatCurrency) => await Call<EstimatedPricesModel>(GetPricesFunc, new GetPricesArgs(RESTClient, currencies, fiatCurrency));

        protected abstract Func<GetPricesArgs, Task<EstimatedPricesModel>> GetPricesFunc { get; }
    }
}
