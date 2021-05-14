using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using Microsoft.Extensions.Logging;
using RestClient.Net.Abstractions;
using System;
// ReSharper disable UnusedMember.Global

namespace CryptoCurrency.Net.APIClients
{
    public class DashClient : InsightClientBase, IBlockchainClient
    {
        #region Constructor
        public DashClient(
            CurrencySymbol currency,
            Func<Uri, IClient> restClientFactory,
            ILogger<DashClient> logger) : base(currency, restClientFactory, logger)
        {
        }
        #endregion

        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.Dash };
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Example: https://api.dash.org/insight-api/addr/[Address]
        /// </summary>
        protected override Uri BaseUriPath => new("https://api.dash.org");
        #endregion
    }
}
