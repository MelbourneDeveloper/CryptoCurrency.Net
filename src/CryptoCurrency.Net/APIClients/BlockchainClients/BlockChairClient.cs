using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{

    public class BlockChairClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new() { CurrencySymbol.BitcoinCash };
        #endregion

        #region Constructor
        public BlockChairClient(CurrencySymbol currency, CreateClient restClientFactory, ILogger<BlockChairClient> logger) : base(currency, restClientFactory, logger)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = restClientFactory(GetType().Name, (o) => o.BaseUrl = new("https://api.blockchair.com"));
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
