using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Octgo;
using RestClient.Net;
using RestClient.Net.Abstractions;
namespace CryptoCurrency.Net.APIClients
{
    public class OtcgoClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { new CurrencySymbol(CurrencySymbol.NEOSymbolName) };
        #endregion

        #region Constructor
        public OtcgoClient(CurrencySymbol currency, IClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = (Client)RESTClientFactory.CreateClient(nameof(OtcgoClient));
            RESTClient.BaseUri = new Uri("https://otcgo.cn");
        }
        #endregion

        #region Func
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            Address addressModel = await RESTClient.GetAsync<Address>($"/api/v1/balances/{address}");
            var balance = addressModel.balances.FirstOrDefault(b => b.name == CurrencySymbol.NEOSymbolName);
            return balance != null ? new BlockChainAddressInformation(address, null, balance.total) : null;
        }
        #endregion
    }
}
