using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using CryptoCurrency.Net.APIClients.Model.Zcha;
using System;
using System.Net;
using System.Threading.Tasks;
using RestClient.Net;
using RestClient.Net.Abstractions;
using Microsoft.Extensions.Logging;

namespace CryptoCurrency.Net.APIClients
{
    public class ZchaClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new() { CurrencySymbol.ZCash };
        #endregion

        #region Constructor
        public ZchaClient(CurrencySymbol currency, Func<Uri, IClient> restClientFactory,
            ILogger<ZchaClient> logger) : base(currency, restClientFactory, logger)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            var baseUri = new Uri("https://api.zcha.in");
            RESTClient = RESTClientFactory(baseUri);
            RESTClient.BaseUri = baseUri;
        }
        #endregion

        #region Func
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            try
            {
                Address addressModel = await RESTClient.GetAsync<Address>($"/v2/mainnet/accounts/{address}");
                return new BlockChainAddressInformation(address, addressModel.balance, addressModel.totalRecv == 0);
            }
            catch (HttpStatusException hex)
            {
                if (hex.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    Logger.LogError(hex, "ZEC Blockchain Error: {response}", hex.Response.GetResponseData());

                    //TODO: Is this correct?
                    return null;
                }

                throw;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error getting ZCash address");
                throw;
            }
        }
        #endregion
    }
}
