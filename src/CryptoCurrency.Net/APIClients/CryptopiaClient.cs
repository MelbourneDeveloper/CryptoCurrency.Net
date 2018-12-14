using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Cryptopia;
using Newtonsoft.Json;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients
{
    public class CryptopiaClient : ExchangeAPIClientBase, IExchangeAPIClient
    {
        #region Constants
        private const string BalanceRequestUrl = @"/Api/GetBalance";
        #endregion

        #region Constructor
        public CryptopiaClient(string apiKey, string apiSecret, IRestClientFactory restClientFactory) : base(apiKey, apiSecret, restClientFactory)
        {
            RESTClient = restClientFactory.CreateRESTClient(new Uri("https://www.cryptopia.co.nz/api"));
        }
        #endregion

        #region Implementation

        /// <inheritdoc />
        public override async Task<GetHoldingsResult> GetHoldings(object tag)
        {
            var accountHoldings = await Balances();

            var retVal = new GetHoldingsResult(tag);

            //TODO: Why do we get nulls in Android?
            foreach (var balance in accountHoldings.Data.Where(b => b != null && b.Total > 0))
            {
                retVal.Result.Add(new CurrencyHolding(new CurrencySymbol(balance.Symbol), new BlockChainAddressInformation(null, balance.Total, false)));
            }

            return retVal;
        }

        public override async Task<Collection<ExchangePairPrice>> GetPairs(CurrencySymbol baseSymbol, PriceType priceType)
        {
            if (baseSymbol == null)
            {
                throw new ArgumentNullException(nameof(baseSymbol));
            }

            var retVal = new Collection<ExchangePairPrice>();
            var prices = await RESTClient.GetAsync<Prices>($"/api/GetMarkets/{baseSymbol.Name}");

            foreach (var pair in prices.Data)
            {
                var toSymbolName = pair.Label.Substring(0, 3);
                var baseSymbolName = pair.Label.Substring(4, pair.Label.Length - 4);

                var currentBaseSymbol = new CurrencySymbol(baseSymbolName);
                if (!currentBaseSymbol.Equals(baseSymbol))
                {
                    continue;
                }

                if (pair.LastPrice == 0)
                {
                    continue;
                }

                //Duplicate coin name
                if (toSymbolName == "BTG" || toSymbolName == "BAT" || toSymbolName == "PLC" || toSymbolName == "CMT" || toSymbolName == "ACC")
                {
                    continue;
                }

                retVal.Add(new ExchangePairPrice(pair.Volume) { BaseSymbol = currentBaseSymbol, ToSymbol = new CurrencySymbol(toSymbolName), Price = priceType == PriceType.Ask ? pair.AskPrice : pair.BidPrice });
            }

            return retVal;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Private IR Call: GetAccounts
        /// </summary>
        /// <returns></returns>
        private async Task<Balances> Balances()
        {
            RESTClient.Headers.Clear();

            const string requestUri = "https://www.cryptopia.co.nz/Api/GetBalance";

            var postData = new
            {
            };

            // Create Request
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8,
                    "application/json")
            };

            // Authentication
            var requestContentBase64String = string.Empty;
            if (request.Content != null)
            {
                // Hash content to ensure message integrity
                using (var md5 = MD5.Create())
                {
                    requestContentBase64String = Convert.ToBase64String(md5.ComputeHash(await request.Content.ReadAsByteArrayAsync()));
                }
            }

            //create random nonce for each request
            var nonce = Guid.NewGuid().ToString("N");

            //Creating the raw signature string
            var signature = Encoding.UTF8.GetBytes(string.Concat(ApiKey, HttpMethod.Post, HttpUtility.UrlEncode(request.RequestUri.AbsoluteUri.ToLower()), nonce, requestContentBase64String));
            RESTClient.Headers.Clear();
            using (var hmac = new HMACSHA256(Convert.FromBase64String(ApiSecret)))
            {
                RESTClient.Authorization = new AuthenticationHeaderValue("amx", $"{ApiKey}:{Convert.ToBase64String(hmac.ComputeHash(signature))}:{nonce}");
            }

            var retVal =  await RESTClient.PostAsync<Balances, string>("{}", BalanceRequestUrl);
            return retVal;
        }
        #endregion
    }
}