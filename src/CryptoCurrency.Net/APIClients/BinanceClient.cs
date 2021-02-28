using CryptoCurrency.Net.Base.Abstractions.APIClients;
using CryptoCurrency.Net.Base.Model;
using CryptoCurrency.Net.Helpers;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Binance;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoCurrency.Net.Base.Extensions;

namespace CryptoCurrency.Net.APIClients
{
    public class BinanceClient : ExchangeAPIClientBase, IExchangeAPIClient
    {
        #region Constructor
        public BinanceClient(string apiKey, string apiSecret, Func<Uri, IClient> restClientFactory) : base(apiKey, apiSecret, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            var baseUri = new Uri("https://api.binance.com");
            var restClient = RESTClientFactory(baseUri);
            restClient.BaseUri = baseUri;
            RESTClient = restClient;
        }
        #endregion

        #region Public Methods
        public override async Task<GetHoldingsResult> GetHoldings(object tag)
        {
            var retVal = new GetHoldingsResult(tag);

            var account = await GetAccount();

            var balances = account.balances.Where(b => b.CompleteValue > 0).ToList();

            foreach (var balance in balances)
            {
                var currencyHolding = new CurrencyHolding(new CurrencySymbol(balance.asset));

                currencyHolding.BlockChainAddresses.Add(new BlockChainAddressInformation(null, balance.CompleteValue, false));

                retVal.Result.Add(currencyHolding);
            }

            return retVal;
        }

        public override async Task<Collection<ExchangePairPrice>> GetPairs(CurrencySymbol baseSymbol, PriceType priceType)
        {
            var retVal = new Collection<ExchangePairPrice>();
            Collection<PairPrice> prices = await RESTClient.GetAsync<Collection<PairPrice>>("/api/v1/ticker/price");

            foreach (var price in prices)
            {
                var toSymbolName = price.symbol.Substring(0, price.symbol.Length - 3);
                var baseSymbolName = price.symbol.Substring(price.symbol.Length - 3, 3);

                var currentBaseSymbol = new CurrencySymbol(baseSymbolName);

                if (baseSymbol != null)
                {
                    if (!currentBaseSymbol.Equals(baseSymbol))
                    {
                        continue;
                    }
                }

                retVal.Add(new ExchangePairPrice(null) { BaseSymbol = baseSymbol, ToSymbol = new CurrencySymbol(toSymbolName), Price = price.price });
            }

            return retVal;
        }
        #endregion

        #region Private Methods
        private async Task<Account> GetAccount()
        {
            //Whacky stuff...
            //No idea why any of this is necessary, but code was pieces together from Binance.NET
            var startTime = DateTime.Now;
            BinanceTime binanceTimeModel = await RESTClient.GetAsync<BinanceTime>("/api/v1/time");
            var binanceTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(binanceTimeModel.serverTime);
            var timeTaken = DateTime.Now - startTime;
            var timeOffset = (binanceTime - DateTime.Now).TotalMilliseconds - (timeTaken.TotalMilliseconds / 2);
            var timestamp = APIHelpers.GetUnixTimestamp(DateTime.UtcNow.AddMilliseconds(timeOffset)).ToString();
            var queryString = $"api/v3/account?recvWindow=10000000000&timestamp={timestamp}";
            var uri = new Uri($"{RESTClient.BaseUri}{queryString}");
            var hmacAsBytes = APIHelpers.GetHashAsBytes(uri.Query.Replace("?", ""), ApiSecret, APIHelpers.HashAlgorithmType.HMACEightBit, Encoding.UTF8);
            queryString += $"&signature={hmacAsBytes.ToHexString()}";
            RESTClient.DefaultRequestHeaders.Clear();
            RESTClient.DefaultRequestHeaders.Add("X-MBX-APIKEY", ApiKey);
            return await RESTClient.GetAsync<Account>(queryString);
        }

        #endregion
    }
}
