using System;
using System.Threading.Tasks;
using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.DogeChain;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients
{
    public class DogeChainClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new CurrencyCapabilityCollection {CurrencySymbol.DogeCoin };
        #endregion

        #region Constructor
        public DogeChainClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            RESTClient = (RestClient)restClientFactory.CreateRESTClient(new Uri("https://dogechain.info"));
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
                var received = await RESTClient.GetAsync<Received>($"/api/v1/address/received/{address}");
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
