using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using Microsoft.Extensions.Logging;
using RestClient.Net.Abstractions;
using System;
// ReSharper disable UnusedMember.Global

namespace CryptoCurrency.Net.APIClients
{
    public class VertcoinClient : SomeClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new() { CurrencySymbol.VertCoin };
        #endregion

        #region Protected Properties
        protected override string BaseUriPath => "http://explorer.vertcoin.info";
        #endregion

        #region Constructor
        public VertcoinClient(
            CurrencySymbol currency,
            Func<Uri, IClient> restClientFactory,
            ILogger<VertcoinClient> logger) : base(currency, restClientFactory, logger)
        {
        }
        #endregion
    }
}
