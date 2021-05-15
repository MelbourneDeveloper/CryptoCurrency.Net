using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using Microsoft.Extensions.Logging;
using RestClient.Net.Abstractions;
using System;

namespace CryptoCurrency.Net.APIClients
{
    /// <summary>
    /// http://explorer2.adalite.io
    /// </summary>
    public class AdaLiteClient : CardanoExplorerBase, IBlockchainClient
    {

        #region Public Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.Cardano };
        #endregion

        public AdaLiteClient(
            CurrencySymbol currency,
            CreateClient restClientFactory,
            ILogger<AdaLiteClient> logger) : base(currency, restClientFactory, logger)
        {
        }

        public override Uri BaseAddress => new("http://explorer2.adalite.io");
    }
}
