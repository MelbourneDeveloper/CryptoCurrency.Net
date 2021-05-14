using System;
using System.Threading.Tasks;
using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using Microsoft.Extensions.Logging;
using RestClient.Net;
using RestClient.Net.Abstractions;
using CryptoCurrency.Net.APIClients.Model.SomeClient;

namespace CryptoCurrency.Net.APIClients
{
    public abstract class SomeClientBase : BlockchainClientBase
    {
        #region Constructor

        protected SomeClientBase(
            CurrencySymbol currency,
            Func<Uri, IClient> restClientFactory,
            ILogger logger) : base(currency, restClientFactory, logger)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = RESTClientFactory(new Uri(BaseUriPath));
            RESTClient.BaseUri = new Uri(BaseUriPath);
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
