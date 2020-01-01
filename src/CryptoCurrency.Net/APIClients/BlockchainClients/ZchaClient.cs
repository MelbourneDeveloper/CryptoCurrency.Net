using System;
using System.Net;
using System.Threading.Tasks;
using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Zcha;
using RestClient.Net;
using RestClient.Net.Abstractions;

namespace CryptoCurrency.Net.APIClients
{
    public class ZchaClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new CurrencyCapabilityCollection { CurrencySymbol.ZCash };
        #endregion

        #region Constructor
        public ZchaClient(CurrencySymbol currency, IClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = (RestClient)RESTClientFactory.CreateClient(new Uri("https://api.zcha.in"));
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
                if (hex.RestResponse.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    Logger.Log($"ZEC Blockchain Error: {hex.RestResponse.GetResponseData()}", null, LogSection);

                    //TODO: Is this correct?
                    return null;
                }

                throw;
            }
            catch (Exception ex)
            {
                Logger.Log("Error getting ZCash address", ex, LogSection);
                throw;
            }
        }
        #endregion
    }
}
