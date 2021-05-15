using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.APIClients.Model.CardanoExplorer;
using CryptoCurrency.Net.Base.Model;
using Microsoft.Extensions.Logging;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Threading.Tasks;
using Urls;

namespace CryptoCurrency.Net.APIClients
{
    public abstract class CardanoExplorerBase : BlockchainClientBase
    {
        public abstract Uri BaseAddress { get; }

        #region Constructor
        protected CardanoExplorerBase(
            CurrencySymbol currency,
            CreateClient restClientFactory,
            ILogger logger) : base(currency, restClientFactory, logger)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = restClientFactory(GetType().Name, (o) => o.BaseUrl = BaseAddress.ToAbsoluteUrl());
        }

        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            Address addressResult = await RESTClient.GetAsync<Address>($"api/addresses/summary/{address}");
            return new BlockChainAddressInformation(address, addressResult.Right.caBalance.getCoin * (decimal).000001, addressResult.Right.caTxList.Count);
        }
        #endregion
    }
}
