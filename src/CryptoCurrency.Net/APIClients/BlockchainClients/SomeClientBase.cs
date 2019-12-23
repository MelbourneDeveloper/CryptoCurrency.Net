using System;
using System.Threading.Tasks;
using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.SomeClient;
using RestClientDotNet;
using RestClientDotNet.Abstractions;

namespace CryptoCurrency.Net.APIClients
{
    public abstract class SomeClientBase : BlockchainClientBase
    {
        #region Constructor

        protected SomeClientBase(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = (RestClient)restClientFactory.CreateRestClient(new Uri(BaseUriPath));
            Currency = currency;
        }
        #endregion

        #region Protected Overridable Properties
        protected abstract string BaseUriPath { get; }
        #endregion

        #region Func
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            Address addressModel = await RESTClient.GetAsync<Address>($"/ext/getaddress/{address}");
            var retVal = new BlockChainAddressInformation(address, addressModel.balance, addressModel.received == 0);
            return retVal;
        }
        #endregion
    }
}
