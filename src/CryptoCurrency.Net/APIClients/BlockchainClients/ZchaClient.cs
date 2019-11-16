using System;
using System.Net;
using System.Threading.Tasks;
using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Zcha;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients
{
    public class ZchaClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new CurrencyCapabilityCollection { CurrencySymbol.ZCash };
        #endregion

        #region Constructor
        public ZchaClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            RESTClient = (RestClient)restClientFactory.CreateRESTClient(new Uri("https://api.zcha.in"));

            //When this client can't see the address it returns "null" with a status code of 404 so we just return a blank address instead
            //TODO: Check that this isn't just returning null for all ZEC addresses
            RESTClient.HttpStatusCodeFuncs.Add(HttpStatusCode.NotFound, data =>
            {
                //TODO: This is just a byte array. It needs to be converted to text and probably deserialized
                Logger.Log($"ZEC Blockchain Error: {data}", null, LogSection);
                return new Address();
            });
        }
        #endregion

        #region Func
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            try
            {
                var addressModel = await RESTClient.GetAsync<Address>($"/v2/mainnet/accounts/{address}");
                return new BlockChainAddressInformation(address, addressModel.balance, addressModel.totalRecv == 0);
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
