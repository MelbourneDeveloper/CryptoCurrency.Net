using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using CryptoCurrency.Net.Helpers;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.IndependentReserve;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients
{
    public class IndependentReserveClient : ExchangeAPIClientBase, IExchangeAPIClient
    {
        #region Constructor
        public IndependentReserveClient(string apiKey, string apiSecret, IRestClientFactory restClientFactory) : base(apiKey, apiSecret, restClientFactory)
        {
            RESTClient = (RestClient)restClientFactory.CreateRESTClient(new Uri("https://api.independentreserve.com"));
        }
        #endregion

        #region Implementation
        /// <inheritdoc />
        public override async Task<GetHoldingsResult> GetHoldings(object tag)
        {
            var retVal = new GetHoldingsResult(tag);

            var accountHoldings = await GetAccounts();

            accountHoldings = accountHoldings.Where(ac => ac.TotalBalance > 0 && string.Compare(ac.AccountStatus, "active", StringComparison.OrdinalIgnoreCase) == 0).ToList();

            foreach (var accountHolding in accountHoldings)
            {
                retVal.Result.Add(new CurrencyHolding(new CurrencySymbol(accountHolding.CurrencyCode), new BlockChainAddressInformation(null, accountHolding.TotalBalance, false)));
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
        private async Task<List<AccountHolding>> GetAccounts()
        {
            var nonce = APIHelpers.GetNonce();

            dynamic data = new ExpandoObject();
            data.apiKey = ApiKey;
            data.nonce = nonce;

            var getAccountsArgs = new GetAccountsArgs
            {
                apiKey = ApiKey,
                nonce = nonce,
                signature = APIHelpers.GetSignature(RESTClient.BaseUri, "/Private/GetAccounts", data, ApiSecret, APIHelpers.HashAlgorithmType.HMACEightBit)
            };

            return await RESTClient.PostAsync<List<AccountHolding>, GetAccountsArgs>(getAccountsArgs, "Private/GetAccounts");
        }
        #endregion
    }
}
