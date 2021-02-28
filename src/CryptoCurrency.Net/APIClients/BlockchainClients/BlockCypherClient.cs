using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Blockcypher;
using RestClientDotNet;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    public class BlockCypherClient : BlockchainClientBase, IBlockchainClient
    {
        private static SemaphoreSlim _lock = new SemaphoreSlim(1,1);
        private static List<DateTime> _calls = new List<DateTime>();

        #region Public Static Properties
        public static string APIKey { get; set; }

        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection
        {
            CurrencySymbol.DogeCoin,
            CurrencySymbol.Litecoin,
            CurrencySymbol.Ethereum
        };
        #endregion

        #region Constructor
        public BlockCypherClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            RESTClient = new RestClient(new NewtonsoftSerializationAdapter(), new Uri("https://api.blockcypher.com"));
        }
        #endregion

        #region Func Overrides
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            //Do a ToLower on ethereum coins but not other coins
            var isEthereum = CurrencySymbol.IsEthereum(Currency);

            address = isEthereum ? address.ToLower() : address;

            var apiKeyPart = !string.IsNullOrEmpty(APIKey) ? $"?token={APIKey}" : string.Empty;

            //https://www.blockcypher.com/dev/bitcoin/#rate-limits-and-tokens
            var balanceModel = await RESTClient.GetAsync<Address>(_lock, _calls, $"v1/{Currency.Name.ToLower()}/main/addrs/{address}/balance{apiKeyPart}", 3);

            //This website returns satoshis/wei so need to divide
            var balance = isEthereum ? balanceModel.balance / CurrencySymbol.Wei : balanceModel.balance / CurrencySymbol.Satoshi;

            return new BlockChainAddressInformation(address, balance, balanceModel.final_n_tx);
        }
        #endregion
    }
}
