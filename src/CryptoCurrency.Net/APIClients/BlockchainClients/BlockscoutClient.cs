using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.APIClients.BlockchainClients.CallArguments;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Blockscout;
using RestClientDotNet; using RestClientDotNet.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    //TODO: this api seems to support tokens for Ethereum classic. Should implement tokens here
    //TODO: GraphQL: https://blockscout.com/etc/mainnet/graphiql

    /// <summary>
    /// https://blockscout.com/etc/mainnet/api_docs
    /// </summary>
    public class BlockscoutClient : BlockchainClientBase, IBlockchainClient
    {
        #region Public Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.EthereumClassic };
        #endregion

        #region Constructor
        public BlockscoutClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = (RestClient)restClientFactory.CreateRestClient(new Uri("https://blockscout.com/etc/mainnet/api"));
        }

        protected override Func<GetAddressesArgs, Task<IEnumerable<BlockChainAddressInformation>>> GetAddressesFunc { get; } = async getAddressesArgs =>
        {
            var addresses = string.Join(",", getAddressesArgs.Addresses);
            var balanceMulti = await getAddressesArgs.RESTClient.GetAsync<BalanceMulti>($"?module=account&action=balancemulti&address={addresses}");
            var returnValue = new List<BlockChainAddressInformation>();

            var tasks = getAddressesArgs.Addresses.Select(a => GetTransactions(a, getAddressesArgs.RESTClient));
            var txLists = await Task.WhenAll(tasks);

            foreach (var result in balanceMulti.result)
            {
                var balance = BigInteger.Parse(result.balance);

                //TODO
                //Warning: precision is lost here.
                //https://stackoverflow.com/questions/11859111/biginteger-division-in-c-sharp#_=_
                var balanceAsEthDouble = Math.Exp(BigInteger.Log(balance) - BigInteger.Log((long)CurrencySymbol.Wei));

                var ethBalance = Math.Round((decimal)balanceAsEthDouble, 18);

                var txList = txLists.FirstOrDefault(l => string.Compare(l.Address, result.account, StringComparison.OrdinalIgnoreCase) == 0);

                returnValue.Add(new BlockChainAddressInformation(result.account, ethBalance, txList.result.Count));
            }

            return returnValue;
        };

        /// <summary>
        /// TODO: Implement this so that transactions can be returned
        /// </summary>
        private static async Task<TxList> GetTransactions(string address, RestClient restClient)
        {
            var result = await restClient.GetAsync<TxList>($"?module=account&action=txlist&address={address}");
            result.Address = address;
            return result;
        }

        public override Task<BlockChainAddressInformation> GetAddress(string address)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
