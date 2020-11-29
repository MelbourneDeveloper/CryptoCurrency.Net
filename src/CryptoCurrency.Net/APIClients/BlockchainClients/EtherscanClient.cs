using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.APIClients.BlockchainClients.CallArguments;
using CryptoCurrency.Net.Ethereum;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Etherscan;
using RestClientDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    /// <summary>
    /// https://etherscan.io/apis#accounts
    /// </summary>
    public class EtherscanClient : BlockchainClientBase, IBlockchainClient
    {
        private static List<DateTime> _calls = new List<DateTime>();
        private static SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        #region Public Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.Ethereum };
        #endregion

        #region Constructor
        public EtherscanClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            RESTClient = (RestClient)restClientFactory.CreateRESTClient(new Uri("http://api.etherscan.io/"));
        }
        #endregion

        protected override Func<GetAddressesArgs, Task<IEnumerable<BlockChainAddressInformation>>> GetAddressesFunc { get; } = async getAddressesArgs =>
        {
            string queryString = $"api?module=account&action=balancemulti&address={string.Join(",", getAddressesArgs.Addresses.Select(a => a.ToLower()))}&tag=latest&apikey=YourApiKeyToken";

            var accountResponse = await ApiGetAsync<ApiResponse<Account>>(getAddressesArgs.RESTClient, queryString);

            foreach (var account in accountResponse.result)
            {
                if (account.balance == "0")
                {
                    queryString = $"api?module=account&action=txlist&address={account.account}&apikey=YourApiKeyToken";
                    var transactionResponse = await ApiGetAsync<ApiResponse<TransactionObject>>(getAddressesArgs.RESTClient, queryString);
                    account.IsEmpty = transactionResponse.result.Count == 0;
                }
            }

            return accountResponse.result.Select(r => new BlockChainAddressInformation(r.account, r.balance.ToEthereumBalance(), r.IsEmpty));

        };

        public override Task<BlockChainAddressInformation> GetAddress(string address)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ensures that we don't call the API more than 5 times per second
        /// Note: the API seems to do this for us anyway. This is just in case
        /// </summary>
        private async static Task<T> ApiGetAsync<T>(IRestClient restClient, string queryString)
        {
            try
            {
                await _lock.WaitAsync();


                if (_calls.Where(d => d > DateTime.Now.AddSeconds(-1)).Count() >= 5)
                {
                    await Task.Delay(1000);
                    _calls.Clear();
                }

                _calls.Add(DateTime.Now);

                return await restClient.GetAsync<T>(new Uri(queryString, UriKind.Relative));
            }
            finally
            {
                _lock.Release();
            }
        }
    }
}
