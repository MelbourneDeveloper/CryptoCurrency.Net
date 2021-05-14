using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using CryptoCurrency.Net.APIClients.Model.Ripple;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RestClient.Net;
using RestClient.Net.Abstractions;
using Microsoft.Extensions.Logging;

namespace CryptoCurrency.Net.APIClients
{
    public class RippleClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { new CurrencySymbol(CurrencySymbol.RippleSymbolName) };
        #endregion

        #region Constructor
        public RippleClient(CurrencySymbol currency, Func<Uri, IClient> restClientFactory, ILogger<RippleClient> logger) : base(currency, restClientFactory, logger)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            var baseUri = new Uri("https://data.ripple.com");
            RESTClient = RESTClientFactory(baseUri);
            RESTClient.BaseUri = baseUri;
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
                if (hex.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    return new BlockChainAddressInformation(address, 0, true);
                }

                throw;
            }
        }
        #endregion
    }
}
