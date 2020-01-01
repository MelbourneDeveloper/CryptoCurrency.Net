using System.Collections.Generic;
using CryptoCurrency.Net.Model;
using RestClient.Net;
namespace CryptoCurrency.Net.APIClients.PriceEstimationClients
{
    public class GetPricesArgs
    {
        public RestClient RESTClient { get; }
        public IEnumerable<CurrencySymbol> Currencies { get; }
        public string FiatCurrency { get; }

        public GetPricesArgs(RestClient restClient, IEnumerable<CurrencySymbol> currencies, string fiatCurrency)
        {
            RESTClient = restClient;
            Currencies = currencies;
            FiatCurrency = fiatCurrency;
        }
    }
}