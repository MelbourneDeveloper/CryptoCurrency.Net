using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Ripple;
using RestClient.Net;
using RestClient.Net.Abstractions;

namespace CryptoCurrency.Net.APIClients
{
    public class RippleClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { new CurrencySymbol(CurrencySymbol.RippleSymbolName) };
        #endregion

        #region Constructor
        public RippleClient(CurrencySymbol currency, IClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = (RestClient)RESTClientFactory.CreateClient(new Uri("https://data.ripple.com"));
        }
        #endregion

        #region Func
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            try
            {
                Address addressModel = await RESTClient.GetAsync<Address>($"/v2/accounts/{address}/balances");
                var balance = addressModel.balances.FirstOrDefault();
                return balance == null ? null : new BlockChainAddressInformation(address, balance.value, false);
            }
            catch (HttpStatusException hex)
            {
                if (hex.RestResponse.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    return new BlockChainAddressInformation(address, 0, true);
                }

                throw;
            }
        }
        #endregion
    }
}
