using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.CardanoExplorer;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    public abstract class CardanoExplorerBase : BlockchainClientBase
    {
        public abstract Uri BaseAddress { get; }

        #region Constructor
        protected CardanoExplorerBase(CurrencySymbol currency, Func<Uri, IClient> restClientFactory) : base(currency, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = RESTClientFactory(baseUri);
            RESTClient.BaseUri = BaseAddress;
        }

        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            Address addressResult = await RESTClient.GetAsync<Address>($"api/addresses/summary/{address}");
            return new BlockChainAddressInformation(address, addressResult.Right.caBalance.getCoin * (decimal).000001, addressResult.Right.caTxList.Count);
        }
        #endregion
    }
}
