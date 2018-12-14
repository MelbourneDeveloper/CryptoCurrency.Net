using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.APIClients.BlockchainClients.CallArguments;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.SomeClient2;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients
{
    public abstract class SomeClient2Base : BlockchainClientBase
    {
        #region Constructor

        protected SomeClient2Base(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            RESTClient = restClientFactory.CreateRESTClient(new Uri(BaseUriPath));
            Currency = currency;
        }
        #endregion

        #region Protected Overridable Properties
        protected abstract string BaseUriPath { get; }
        #endregion

        #region Protected Overridable Methods
        protected string GetAddressesUrlPart(IEnumerable<string> addresses)
        {
            return string.Join("|", addresses);
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

            var addresses = await getAddressesArgs.RESTClient.GetAsync<AddressResult>(queryString);

            var retVal = new List<BlockChainAddressInformation>();

            foreach (var address in addresses.addresses)
            {
                retVal.Add(new BlockChainAddressInformation(address.address, address.final_balance / CurrencySymbol.Satoshi, address.n_tx));
            }

            foreach (var address in getAddressesArgs.Addresses)
            {
                var addressInfo = retVal.FirstOrDefault(a => string.Equals(a.Address, address, StringComparison.CurrentCultureIgnoreCase));
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
