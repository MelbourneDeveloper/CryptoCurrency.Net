using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
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

        public AdaLiteClient(CurrencySymbol currency, Func<Uri, IClient> restClientFactory) : base(currency, restClientFactory)
        {
        }

        public override Uri BaseAddress => new Uri("http://explorer2.adalite.io");
    }
}
