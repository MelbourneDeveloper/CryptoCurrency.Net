using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using CryptoCurrency.Net.APIClients.Model.Steller;
using System;
using System.Linq;
using System.Threading.Tasks;
using RestClient.Net;
using RestClient.Net.Abstractions;
using Microsoft.Extensions.Logging;

namespace CryptoCurrency.Net.APIClients
{
    public class StellerClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new() { new CurrencySymbol(CurrencySymbol.StellerSymbolName) };
        #endregion

        #region Constructor
        public StellerClient(
            CurrencySymbol currency,
            CreateClient restClientFactory,
            ILogger<StellerClient> logger) : base(currency, restClientFactory, logger)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = restClientFactory(GetType().Name, (o) => o.BaseUrl = new("https://horizon.stellar.org"));
        }
        #endregion

        #region Func
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            Account addressModel = await RESTClient.GetAsync<Account>($"/accounts/{address}");

            //TODO: Get a transaction list?
            return new BlockChainAddressInformation(addressModel.account_id, null, addressModel.balances.FirstOrDefault().balance);
        }
        #endregion
    }
}
