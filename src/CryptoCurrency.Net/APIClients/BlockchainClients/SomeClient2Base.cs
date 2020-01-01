using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.APIClients.BlockchainClients.CallArguments;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.SomeClient2;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    public abstract class SomeClient2Base : BlockchainClientBase
    {
        #region Constructor

        protected SomeClient2Base(CurrencySymbol currency, IClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = (Client)RESTClientFactory.CreateClient(BaseUriPath.ToString());
            RESTClient.BaseUri = new Uri(BaseUriPath);
            Currency = currency;
        }
        #endregion

        #region Protected Overridable Properties
#pragma warning disable CA1056 
        protected abstract string BaseUriPath { get; }
#pragma warning restore CA1056 
        #endregion

        #region Protected Static Methods
        protected static string GetAddressesUrlPart(IEnumerable<string> addresses)
        {
            return string.Join("|", addresses);
        }
        #endregion

        #region Public Override Methods
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            var blockchainInfos = await GetAddressesFunc(new GetAddressesArgs(RESTClient, new List<string> { address }, Currency, this));
            return blockchainInfos.FirstOrDefault();
        }
        #endregion

        #region Protected Abstract Methods
        protected abstract string GetQueryString(string addressesPart);
        #endregion

        #region Func
        protected override Func<GetAddressesArgs, Task<IEnumerable<BlockChainAddressInformation>>> GetAddressesFunc { get; } = async getAddressesArgs =>
        {
            var addressesPart = string.Join("|", getAddressesArgs.Addresses.Select(a => a));

            var queryString = ((SomeClient2Base)getAddressesArgs.Client).GetQueryString(addressesPart);

            AddressResult addresses = await getAddressesArgs.RESTClient.GetAsync<AddressResult>(queryString);

            var retVal = new List<BlockChainAddressInformation>();

            foreach (var address in addresses.addresses)
            {
                retVal.Add(new BlockChainAddressInformation(address.address, address.final_balance / CurrencySymbol.Satoshi, address.n_tx));
            }

            foreach (var address in getAddressesArgs.Addresses)
            {
                var addressInfo = retVal.FirstOrDefault(a => string.Equals(a.Address, address, StringComparison.OrdinalIgnoreCase));
                if (addressInfo == null)
                {
                    //The server did not return a result for this address. This may be that he address is invalid but more likely that is just not used, so just report that
                    retVal.Add(new BlockChainAddressInformation(address, 0, true));
                }
            }

            return retVal;
        };
        #endregion
    }
}
