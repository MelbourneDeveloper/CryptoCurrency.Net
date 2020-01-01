using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.APIClients.BlockchainClients.CallArguments;
using CryptoCurrency.Net.BCH;
using CryptoCurrency.Net.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    public class BtcCom : BlockchainClientBase, IBlockchainClient
    {
        #region Constructor
        public BtcCom(CurrencySymbol currency, IClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = (Client)RESTClientFactory.CreateClient(nameof(BtcCom));
            RESTClient.BaseUri = new Uri("https://bch-chain.api.btc.com");
        }
        #endregion

        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.BitcoinCash };
        #endregion

        #region Overrides
        protected override Func<GetAddressesArgs, Task<IEnumerable<BlockChainAddressInformation>>> GetAddressesFunc { get; } = async getAddressesArgs =>
        {
            //https://dev.btc.com/docs/js - 300 request per minute
            var delay = 60000 / 299;
            await Task.Delay(delay);

            var blockChainAddressInformations = new List<BlockChainAddressInformation>();

            var addresses = getAddressesArgs.Addresses.Select(ad => AddressConverter.ToOldFormat(ad).Address);
            var addressesString = string.Join(",", addresses);
            var json = await getAddressesArgs.RESTClient.GetAsync<string>($"v3/address/{addressesString}");

            var rootJObject = (JObject)JsonConvert.DeserializeObject(json);
            var dataJToken = rootJObject["data"];

            foreach (var addressJToken in dataJToken)
            {
                var address = addressJToken["address"].ToString();
                var balance = long.Parse(addressJToken["balance"].ToString()) / CurrencySymbol.Satoshi;

                blockChainAddressInformations.Add(
                new BlockChainAddressInformation
                {
                    Address = AddressConverter.ToNewFormat(address, false).Address,
                    Balance = balance,
                    TransactionCount = int.Parse(addressJToken["tx_count"].ToString())
                });
            }

            return blockChainAddressInformations;
        };

        public override Task<BlockChainAddressInformation> GetAddress(string address)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
