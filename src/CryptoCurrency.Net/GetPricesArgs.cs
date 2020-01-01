using System.Collections.Generic;
using CryptoCurrency.Net.Model;
using RestClient.Net;
namespace CryptoCurrency.Net.APIClients.PriceEstimationClients
{
    public class GetPricesArgs
    {
        public Client RESTClient { get; }
        public IEnumerable<CurrencySymbol> Currencies { get; }
        public string FiatCurrency { get; }

        public GetPricesArgs(Client client, IEnumerable<CurrencySymbol> currencies, string fiatCurrency)
        {
            RESTClient = client;
            Currencies = currencies;
            FiatCurrency = fiatCurrency;
        }
    }
}