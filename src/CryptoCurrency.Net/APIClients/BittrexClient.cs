using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoCurrency.Net.Abstractions.APIClients;
using CryptoCurrency.Net.Helpers;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Bittrex;
using RestClient.Net;
using RestClient.Net.Abstractions;

namespace CryptoCurrency.Net.APIClients
{
    public class BittrexClient : ExchangeAPIClientBase, IExchangeAPIClient
    {
        #region Constructor
        public BittrexClient(string apiKey, string apiSecret, Func<Uri, IClient> restClientFactory) : base(apiKey, apiSecret, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            var baseUri = new Uri("https://bittrex.com/");
            RESTClient = RESTClientFactory(baseUri);
            RESTClient.BaseUri = baseUri;
        }
        #endregion

        #region Implementation
        public override async Task<GetHoldingsResult> GetHoldings(object tag)
        {
            var retVal = new GetHoldingsResult(tag);
            var result = await GetBalances();
            if (result.success)
            {
                foreach (var accountHolding in result.result.Where(accountHolding => accountHolding.Balance != 0))
                {
                    retVal.Result.Add(new CurrencyHolding(new CurrencySymbol(accountHolding.Currency), new BlockChainAddressInformation(null, accountHolding.Balance, false)));
                }
            }
            else
            {
                throw new GetHoldingsException("Error connecting to Bittrex");
            }

            return retVal;
        }

        public override async Task<Collection<ExchangePairPrice>> GetPairs(CurrencySymbol baseSymbol, PriceType priceType)
        {
            var retVal = new Collection<ExchangePairPrice>();
            Markets markets = await RESTClient.GetAsync<Markets>("/api/v1.1/public/getmarketsummaries");

            foreach (var pair in markets.result)
            {
                var baseSymbolName = pair.MarketName.Substring(0, 3);
                var toSymbolName = pair.MarketName.Substring(4, pair.MarketName.Length - 4);

                var currentBaseSymbol = new CurrencySymbol(baseSymbolName);

                if (baseSymbol != null)
                {
                    if (!currentBaseSymbol.Equals(baseSymbol))
                    {
                        continue;
                    }
                }

                retVal.Add(new ExchangePairPrice(pair.Volume) { BaseSymbol = currentBaseSymbol, ToSymbol = new CurrencySymbol(toSymbolName), Price = priceType == PriceType.Ask ? pair.Ask : pair.Bid });
            }

            return retVal;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Private Bittrex Call: getbalances
        /// </summary>
        /// <returns></returns>
        private async Task<GetBalancesResult> GetBalances()
        {
            var nonce = APIHelpers.GetNonce();

            var uri = $"https://bittrex.com/api/v1.1/account/getbalances?apikey={ApiKey}&nonce={nonce}";

            var hmac = APIHelpers.GetHash(uri, ApiSecret, APIHelpers.HashAlgorithmType.HMACNineBit, Encoding.ASCII);

            RESTClient.DefaultRequestHeaders.Clear();
            RESTClient.DefaultRequestHeaders.Add("apisign", hmac);

            //TODO: Remove GetBalancesArgs. No idea why we are passing GetBalancesArgs in...
            return await RESTClient.PostAsync<GetBalancesResult, object>(new object(), $"https://bittrex.com/api/v1.1//account/getbalances?apikey={ApiKey}&nonce={nonce}");
        }
        #endregion
    }
}
