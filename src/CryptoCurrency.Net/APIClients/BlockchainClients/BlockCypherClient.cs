using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Blockcypher;
using RestClientDotNet;
using System;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    public class BlockCypherClient : BlockchainClientBase, IBlockchainClient
    {
        //1 second / 3 + 20
        private const int MillisecondsDelay = (int)(1000 / (decimal)3) +20;

        #region Public Static Properties
        public static string APIKey { get; set; }

        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection
        {
            CurrencySymbol.DogeCoin,
            CurrencySymbol.Litecoin
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
            if (address == null) throw new ArgumentNullException(nameof(address));

            //https://www.blockcypher.com/dev/bitcoin/#rate-limits-and-tokens
            await Task.Delay(MillisecondsDelay);

            //Do a ToLower on ethereum coins but not other coins
            var isEthereum = CurrencySymbol.IsEthereum(Currency);

            address = isEthereum ? address.ToLower() : address;

            var apiKeyPart = !string.IsNullOrEmpty(APIKey) ? $"?token={APIKey}" : string.Empty;
            var balanceModel = await RESTClient.GetAsync<Address>($"v1/{Currency.Name.ToLower()}/main/addrs/{address}/balance{apiKeyPart}");

            //This website returns satoshis/wei so need to divide
            var balance = isEthereum ? balanceModel.balance / CurrencySymbol.Wei : balanceModel.balance / CurrencySymbol.Satoshi;

            return new BlockChainAddressInformation(address, balance, balanceModel.final_n_tx);
        }
        #endregion
    }
}
