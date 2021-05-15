using CryptoCurrency.Net.APIClients.Model.BTCMarkets;
using CryptoCurrency.Net.Base.Abstractions.APIClients;
using CryptoCurrency.Net.Base.Extensions;
using CryptoCurrency.Net.Base.Model;
using CryptoCurrency.Net.Helpers;
using Microsoft.Extensions.Logging;
using RestClient.Net;
using RestClient.Net.Abstractions;
using RestClient.Net.Abstractions.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Urls;

namespace CryptoCurrency.Net.APIClients
{
    public partial class BTCMarketsClient : ExchangeAPIClientBase, IExchangeAPIClient
    {
        public const string ACCOUNTBALANCEPATH = "/account/balance";

        #region Constructor
        public BTCMarketsClient(
            string apiKey,
            string apiSecret,
            CreateClient restClientFactory,
            ILogger<BTCMarketsClient> logger) : base(apiKey, apiSecret, restClientFactory, logger)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = restClientFactory(GetType().Name,
                (o) =>
                {
                    o.HeadersCollection =
                    HeadersCollection.Empty
                    .Append("Accept", BTCMarketsHeaderConstants.CONTENT)
                    .Append("Accept-Charset", BTCMarketsHeaderConstants.ENCODING)
                    .Append(BTCMarketsHeaderConstants.APIKEY_HEADER, ApiKey);

                    o.BaseUrl = new("https://api.btcmarkets.net");
                }); ;
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

        public override Task<Collection<ExchangePairPrice>> GetPairs(CurrencySymbol baseSymbol, PriceType priceType) => throw new NotImplementedException();

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

            List<Balance> result;

            try
            {
                var headersCollection = BTCMarketsHeaderConstants.TIMESTAMP_HEADER
                    .ToHeadersCollection(timestamp)
                    .Append(BTCMarketsHeaderConstants.SIGNATURE_HEADER, signature);

                result = await RESTClient.GetAsync<List<Balance>>(new RelativeUrl(ACCOUNTBALANCEPATH), headersCollection);
            }
            catch (DeserializationException dex)
            {
                throw new BTCMarketsException(new ErrorResult { errorMessage = dex.ToString() });
            }

            return result;
        }
        #endregion

        private static string BuildStringToSign(string action, string postData, string timestamp)
            => new StringBuilder()
            .Append(action + "\n")
            .Append(timestamp + "\n")
            .Append(postData ?? "").ToString();

        /// <summary>
        /// TODO: Merge in to other compute hash methods
        /// </summary>
        public static string ComputeHash(string privateKey, string data)
        {
            var encoding = Encoding.UTF8;
            using var hasher = new HMACSHA512(Convert.FromBase64String(privateKey));
            return Convert.ToBase64String(hasher.ComputeHash(encoding.GetBytes(data)));
        }
    }
}