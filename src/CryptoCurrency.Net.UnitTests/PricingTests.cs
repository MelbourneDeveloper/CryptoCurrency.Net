using CryptoCurrency.Net.APIClients.PriceEstimationClients;
using CryptoCurrency.Net.Base.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestClient.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CryptoCurrency.Net.UnitTests
{
    [TestClass]
    public class PricingTests
    {
        [TestMethod]
        public async Task GetUSDBitcoinPrice()
        {
            var priceEstimationManager = new PriceEstimationManager((u) => new Client(baseUri: u),
                UnitTestGlobals.LoggerFactory.CreateLogger<PriceEstimationManager>());
            var estimatedPrice = await priceEstimationManager.GetPrices(new List<CurrencySymbol> { CurrencySymbol.Bitcoin }, "USD");
            Console.WriteLine($"Estimate: {estimatedPrice.Result.First().FiatEstimate}");
        }
    }
}
