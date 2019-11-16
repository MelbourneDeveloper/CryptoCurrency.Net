using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestClientDotNet;
using System;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{

    public class BlockChairClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new CurrencyCapabilityCollection { CurrencySymbol.BitcoinCash };
        #endregion

        #region Constructor
        public BlockChairClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = (RestClient)restClientFactory.CreateRESTClient(new Uri("https://api.blockchair.com"));
            Currency = currency;
        }
        #endregion

        #region Func
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            //30x per second is ok
            var delay = 60000 / 29;

            await Task.Delay(delay);

            var json = await RESTClient.GetAsync<string>($"/bitcoin-cash/dashboards/address/{address}");
            var baseObject = (JObject)JsonConvert.DeserializeObject(json);
            var dataObject = baseObject["data"];
            var addressToken = dataObject[address];
            addressToken = addressToken["address"];
            var balanceInSatoshis = (long)addressToken["balance"];

            var transactionCountToken = addressToken["transaction_count"];
            var transactionCount = transactionCountToken.Value<int>();

            return new BlockChainAddressInformation(address, balanceInSatoshis / CurrencySymbol.Satoshi, transactionCount);
        }
        #endregion
    }
}
