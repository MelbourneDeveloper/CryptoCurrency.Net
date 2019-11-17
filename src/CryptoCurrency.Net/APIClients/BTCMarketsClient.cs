using CryptoCurrency.Net.Abstractions.APIClients;
using CryptoCurrency.Net.Helpers;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.BTCMarkets;
using Newtonsoft.Json;
using RestClientDotNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    public partial class BTCMarketsClient : ExchangeAPIClientBase, IExchangeAPIClient
    {
        public const string ACCOUNTBALANCEPATH = "/account/balance";

        #region Constructor
        public BTCMarketsClient(string apiKey, string apiSecret, IRestClientFactory restClientFactory) : base(apiKey, apiSecret, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = (RestClient)restClientFactory.CreateRESTClient(new Uri("https://api.btcmarkets.net"));
        }
        #endregion

        #region Implementation
        /// <inheritdoc />
        public override async Task<GetHoldingsResult> GetHoldings(object tag)
        {
            var retVal = new GetHoldingsResult(tag);

            var accountHoldings = await Balances();

            accountHoldings = accountHoldings.Where(ac => ac.balance > 0).ToList();

            foreach (var accountHolding in accountHoldings)
            {
                retVal.Result.Add(new CurrencyHolding(new CurrencySymbol(accountHolding.currency), new BlockChainAddressInformation(null, accountHolding.balance / CurrencySymbol.Satoshi, false)));
            }

            return retVal;
        }

        public override Task<Collection<ExchangePairPrice>> GetPairs(CurrencySymbol baseSymbol, PriceType priceType)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region Private Methods

        /// <summary>
        /// Private IR Call: GetAccounts
        /// </summary>
        /// <returns></returns>
        private async Task<List<Balance>> Balances()
        {

            //get the epoch timestamp to be used as the nonce
            //BTC Markets moans about timestamps for some damned reason so we need to get the time from ConvertUnixTime
            var timestamp = APIHelpers.GetCurrentUnixTimestamp().ToString();

            // create the string that needs to be signed
            var stringToSign = BuildStringToSign(ACCOUNTBALANCEPATH, null, timestamp);

            // build signature to be included in the http header
            //TODO: API Secret?
            var signature = ComputeHash(ApiSecret, stringToSign);

            lock (RESTClient.Headers)
            {
                RESTClient.Headers.Clear();
                RESTClient.Headers.Add("Accept", BTCMarketsHeaderConstants.CONTENT);
                RESTClient.Headers.Add("Accept-Charset", BTCMarketsHeaderConstants.ENCODING);
                RESTClient.Headers.Add(BTCMarketsHeaderConstants.APIKEY_HEADER, ApiKey);
                RESTClient.Headers.Add(BTCMarketsHeaderConstants.SIGNATURE_HEADER, signature);
                RESTClient.Headers.Add(BTCMarketsHeaderConstants.TIMESTAMP_HEADER, timestamp);
            }

            List<Balance> result;

            try
            {
                result = await RESTClient.GetAsync<List<Balance>>(ACCOUNTBALANCEPATH);
            }
            catch (DeserializationException dex)
            {

                var errorResult = JsonConvert.DeserializeObject<ErrorResult>(dex.Markup);
                throw new BTCMarketsException(errorResult);

            }

            return result;
        }
        #endregion

        private static string BuildStringToSign(string action, string postData, string timestamp)
        {
            var stringToSign = new StringBuilder();
            stringToSign.Append(action + "\n");
            stringToSign.Append(timestamp + "\n");
            if (postData != null)
            {
                stringToSign.Append(postData);
            }

            return stringToSign.ToString();
        }

        /// <summary>
        /// TODO: Merge in to other compute hash methods
        /// </summary>
        public static string ComputeHash(string privateKey, string data)
        {
            var encoding = Encoding.UTF8;
            using (var hasher = new HMACSHA512(Convert.FromBase64String(privateKey)))
            {
                return Convert.ToBase64String(hasher.ComputeHash(encoding.GetBytes(data)));
            }
        }
    }
}