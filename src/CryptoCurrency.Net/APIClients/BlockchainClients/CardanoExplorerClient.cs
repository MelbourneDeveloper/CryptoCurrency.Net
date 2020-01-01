using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
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

        public CardanoExplorerClient(CurrencySymbol currency, IClientFactory restClientFactory) : base(currency, restClientFactory)
        {
        }

        public override Uri BaseAddress => new Uri("https://cardanoexplorer.com");
    }
}
