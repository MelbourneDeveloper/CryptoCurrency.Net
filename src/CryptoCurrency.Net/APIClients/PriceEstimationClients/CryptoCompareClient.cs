using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.PriceEstimatation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients.PriceEstimationClients
{
    public class CryptoCompareClient : PriceEstimationClientBase, IPriceEstimationClient
    {
        public CryptoCompareClient(IRestClientFactory restClientFactory) : base(restClientFactory)
        {
            RESTClient = (RestClient)restClientFactory.CreateRESTClient(new Uri("https://min-api.cryptocompare.com"));
        }

        protected override Func<GetPricesArgs, Task<EstimatedPricesModel>> GetPricesFunc { get; } = async a =>
        {
            var retVal = new EstimatedPricesModel();

            if (a.Currencies.ToList().Count == 0)
            {
                return retVal;
            }

            retVal.LastUpdate = DateTime.Now;

            var symbolsPart = string.Join(",", a.Currencies.Select(c => c.Name));

            var priceJson = await a.RESTClient.GetAsync<string>($"data/pricemultifull?fsyms={symbolsPart}&tsyms={a.FiatCurrency}");

            var jObject = (JObject)JsonConvert.DeserializeObject(priceJson);

            var rawNode = (JObject)jObject.First.First;

            foreach (JProperty coinNode in rawNode.Children())
            {
                var fiatNode = (JProperty)coinNode.First().First;

                var allProperties = fiatNode.First.Children().Cast<JProperty>().ToList();

                var change24HourProperty = allProperties.FirstOrDefault(p => string.Compare(p.Name, "CHANGEPCT24HOUR", true) == 0);
                var priceProperty = allProperties.FirstOrDefault(p => string.Compare(p.Name, "PRICE", true) == 0);

                var price = (decimal)priceProperty.Value;
                var change24Hour = (decimal)change24HourProperty.Value;
                retVal.Result.Add(new CoinEstimate { CurrencySymbol = new CurrencySymbol(coinNode.Name), ChangePercentage24Hour = change24Hour, FiatEstimate = price, LastUpdate = DateTime.Now });
            }

            //Extreme hack. It's better to show zero than nothing at all and get the coins stuck
            foreach (var currency in a.Currencies)
            {
                if (retVal.Result.FirstOrDefault(ce => ce.CurrencySymbol.Equals(currency)) == null)
                {
                    retVal.Result.Add(new CoinEstimate { ChangePercentage24Hour = 0, CurrencySymbol = currency, FiatEstimate = 0, LastUpdate = DateTime.Now });
                }
            }

            return retVal;
        };
    }
}
