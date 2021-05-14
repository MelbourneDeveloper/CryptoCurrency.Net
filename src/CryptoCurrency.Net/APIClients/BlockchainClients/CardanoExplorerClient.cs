using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using Microsoft.Extensions.Logging;
using RestClient.Net.Abstractions;
using System;

namespace CryptoCurrency.Net.APIClients
{
    /// <summary>
    /// https://cardanodocs.com/technical/explorer/api/
    /// </summary>
    public class CardanoExplorerClient : CardanoExplorerBase, IBlockchainClient
    {
        #region Public Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.Cardano };
        #endregion

        public CardanoExplorerClient(
            CurrencySymbol currency,
            Func<Uri, IClient> restClientFactory,
            ILogger<CardanoExplorerClient> logger) : base(currency, restClientFactory, logger)
        {
        }

        public override Uri BaseAddress => new("https://cardanoexplorer.com");
    }
}
