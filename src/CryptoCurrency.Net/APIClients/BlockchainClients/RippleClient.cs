using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Ripple;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients
{
    public class RippleClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { new CurrencySymbol(CurrencySymbol.RippleSymbolName) };
        #endregion

        #region Constructor
        public RippleClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = (RestClient)restClientFactory.CreateRESTClient(new Uri("https://data.ripple.com"));
        }
        #endregion

        #region Func
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            var addressModel = await RESTClient.GetAsync<Address>($"/v2/accounts/{address}/balances");
            var balance = addressModel.balances.FirstOrDefault();

            return balance == null ? null : new BlockChainAddressInformation(address, null, balance.value);
        }
        #endregion
    }
}
