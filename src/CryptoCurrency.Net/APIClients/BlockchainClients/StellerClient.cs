using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using CryptoCurrency.Net.APIClients.Model.Steller;
using RestClientDotNet;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    public class StellerClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new CurrencyCapabilityCollection { new CurrencySymbol(CurrencySymbol.StellerSymbolName) };
        #endregion

        #region Constructor
        public StellerClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = (RestClient)restClientFactory.CreateRESTClient(new Uri("https://horizon.stellar.org"));
        }
        #endregion

        #region Func
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            var addressModel = await RESTClient.GetAsync<Account>($"/accounts/{address}");

            //TODO: Get a transaction list?
            return new BlockChainAddressInformation(addressModel.account_id, null, addressModel.balances.FirstOrDefault().balance);
        }
        #endregion
    }
}
