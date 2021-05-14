using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.APIClients.BlockchainClients.CallArguments;
using CryptoCurrency.Net.Base.Extensions;
using CryptoCurrency.Net.Base.Model;
using CryptoCurrency.Net.Ethereum;
using CryptoCurrency.Net.Model.Etherscan;
using Microsoft.Extensions.Logging;
using RestClient.Net.Abstractions;
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
        private static readonly List<DateTime> _calls = new();
        private static readonly SemaphoreSlim _lock = new(1, 1);

        #region Public Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.Ethereum };
        #endregion

        #region Constructor
        public EtherscanClient(CurrencySymbol currency, Func<Uri, IClient> restClientFactory, ILogger<EtherscanClient> logger) : base(currency, restClientFactory, logger) => RESTClient = restClientFactory(new Uri("http://api.etherscan.io/"));
        #endregion

        protected override Func<GetAddressesArgs, Task<IEnumerable<BlockChainAddressInformation>>> GetAddressesFunc { get; } = async getAddressesArgs =>
        {
            var queryString = $"api?module=account&action=balancemulti&address={string.Join(",", getAddressesArgs.Addresses.Select(a => a.ToLower()))}&tag=latest&apikey=YourApiKeyToken";

            var accountResponse = await getAddressesArgs.RESTClient.GetAsync<ApiResponse<Account>>(_lock, _calls, queryString);

            foreach (var account in accountResponse.result)
            {
                if (account.balance == "0")
                {
                    queryString = $"api?module=account&action=txlist&address={account.account}&apikey=YourApiKeyToken";
                    var transactionResponse = await getAddressesArgs.RESTClient.GetAsync<ApiResponse<TransactionObject>>(_lock, _calls, queryString);
                    account.IsEmpty = transactionResponse.result.Count == 0;
                }
            }

            return accountResponse.result.Select(r => new BlockChainAddressInformation(r.account, r.balance.ToEthereumBalance(), r.IsEmpty));

        };

        public override Task<BlockChainAddressInformation> GetAddress(string address) => throw new NotImplementedException();


    }


}
