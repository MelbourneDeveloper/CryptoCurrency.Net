using System;
using System.Threading.Tasks;
using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.APIClients.Model.DogeChain;
using CryptoCurrency.Net.Base.Model;
using Microsoft.Extensions.Logging;
using RestClient.Net;
using RestClient.Net.Abstractions;

namespace CryptoCurrency.Net.APIClients
{
    public class DogeChainClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new CurrencyCapabilityCollection { CurrencySymbol.DogeCoin };
        #endregion

        #region Constructor
        public DogeChainClient(CurrencySymbol currency, Func<Uri, IClient> restClientFactory, ILogger<DogeChainClient> logger) : base(currency, restClientFactory, logger)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            var baseUri = new Uri("https://dogechain.info");
            RESTClient = RESTClientFactory(baseUri);
            RESTClient.BaseUri = baseUri;
        }
        #endregion

        #region Func
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            bool unused;

            var balance = await RESTClient.GetAsync<decimal>($"/chain/Dogecoin/q/addressbalance/{address}");
            if (balance == 0)
            {
                //There is no balance so check to see if the address was ever used
                Received received = await RESTClient.GetAsync<Received>($"/api/v1/address/received/{address}");
                unused = received.received == 0;
            }
            else
            {
                unused = false;
            }

            return new BlockChainAddressInformation(address, balance, unused);
        }
        #endregion
    }
}
