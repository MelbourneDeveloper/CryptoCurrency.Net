using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.APIClients.BlockchainClients.CallArguments;
using CryptoCurrency.Net.Ethereum;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Etherscan;
using CryptoCurrency.Net.Model.Ethplorer;
using RestClientDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    /// <summary>
    /// https://etherscan.io/apis#accounts
    /// </summary>
    public class EtherscanClient : BlockchainClientBase, IBlockchainClient
    {
        #region Public Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.Ethereum };
        #endregion

        #region Constructor
        public EtherscanClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            RESTClient = (RestClient)restClientFactory.CreateRESTClient(new Uri("http://api.etherscan.io/"));
        }

        protected override Func<GetAddressesArgs, Task<IEnumerable<BlockChainAddressInformation>>> GetAddressesFunc { get; } = async getAddressesArgs =>
        {
            string queryString = $"api?module=account&action=balancemulti&address={string.Join(",", getAddressesArgs.Addresses.Select(a=>a.ToLower()))}&tag=latest&apikey=YourApiKeyToken";

            var balances = await getAddressesArgs.RESTClient.GetAsync<Account>(new Uri(queryString, UriKind.Relative));

            return balances.result.Select(r => new BlockChainAddressInformation(r.account, r.balance.ToEthereumBalance(), false));

        };

        public override Task<BlockChainAddressInformation> GetAddress(string address)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
