using CryptoCurrency.Net.Base.Model;
using CryptoCurrency.Net.Base.Model.PriceEstimatation;
using RestClientDotNet;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients.PriceEstimationClients
{
    public abstract class PriceEstimationClientBase : APIClientBase
    {
        protected PriceEstimationClientBase(IRestClientFactory restClientFactory) : base(restClientFactory)
        {
        }

        public async Task<EstimatedPricesModel> GetPrices(IEnumerable<CurrencySymbol> currencies, string fiatCurrency)
        {
            return await Call<EstimatedPricesModel>(GetPricesFunc, new GetPricesArgs(RESTClient, currencies, fiatCurrency));
        }

        protected abstract Func<GetPricesArgs, Task<EstimatedPricesModel>> GetPricesFunc { get; }
    }
}
