using CryptoCurrency.Net.Model;
using RestClient.Net.Abstractions;
using System.Collections.Generic;

namespace CryptoCurrency.Net.APIClients.PriceEstimationClients
{
    public class GetPricesArgs
    {
        public IClient RESTClient { get; }
        public IEnumerable<CurrencySymbol> Currencies { get; }
        public string FiatCurrency { get; }

        public GetPricesArgs(IClient client, IEnumerable<CurrencySymbol> currencies, string fiatCurrency)
        {
            RESTClient = client;
            Currencies = currencies;
            FiatCurrency = fiatCurrency;
        }
    }
}