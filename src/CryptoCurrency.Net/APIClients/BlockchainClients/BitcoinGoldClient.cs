using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using RestClient.Net.Abstractions;
using System;
// ReSharper disable UnusedMember.Global

namespace CryptoCurrency.Net.APIClients
{
    public class BitcoinGoldClient : InsightClientBase, IBlockchainClient
    {
        #region Constructor
        public BitcoinGoldClient(CurrencySymbol currency, IClientFactory restClientFactory) : base(currency, restClientFactory)
        {
        }
        #endregion

        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.BitcoinGold };
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Example: https://explorer.bitcoingold.org/insight-api/addr/[Address]
        /// </summary>
        protected override Uri BaseUriPath => new Uri("https://explorer.bitcoingold.org");
        #endregion
    }
}
