using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using Microsoft.Extensions.Logging;
using RestClient.Net.Abstractions;
using System;

namespace CryptoCurrency.Net.APIClients
{
    /// <summary>
    ///https://blockchain.info/multiaddr?active=$address|$address
    /// </summary>
    public class BlockchainClient : SomeClient2Base, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new() { CurrencySymbol.Bitcoin };
        #endregion

        #region Protected Override Properties
        protected override string BaseUriPath => "https://blockchain.info";
        #endregion

        #region Constructor
        public BlockchainClient(
            CurrencySymbol currency,
            Func<Uri, IClient> restClientFactory,
            ILogger<BlockchainClient> logger) : base(currency, restClientFactory, logger)
        {
        }
        #endregion

        #region Protected Overrides
        protected override string GetQueryString(string addressesPart) => $"/multiaddr?active={addressesPart}";
        #endregion
    }
}
