using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using RestClient.Net.Abstractions;
using System;

namespace CryptoCurrency.Net.APIClients
{

    /// <summary>
    /// https://chainz.cryptoid.info/
    /// TODO: This supports a tonne of coins...
    /// </summary>
    public class ChainzClient : SomeClient2Base, IBlockchainClient
    {
        #region Public Static String
        public static string APIKey { get; set; }
        #endregion

        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new()
        {
                    CurrencySymbol.Bitcoin,
                    CurrencySymbol.Litecoin,
                    CurrencySymbol.Crown,
                    CurrencySymbol.Dash,
                    CurrencySymbol.DigiByte,
                    CurrencySymbol.VertCoin
                };
        #endregion

        #region Protected Override Properties
        protected override string BaseUriPath => "https://chainz.cryptoid.info";
        #endregion

        #region Constructor
        public ChainzClient(CurrencySymbol currency, Func<Uri, IClient> restClientFactory) : base(currency, restClientFactory)
        {
        }
        #endregion

        #region Protected Override Methods
        protected override string GetQueryString(string addressesPart)
        {
            var apiKeyPart = !string.IsNullOrEmpty(APIKey) ? $"&key={APIKey}" : string.Empty;
            return $"/{Currency.Name.ToLower()}/api.dws?q=multiaddr&active={addressesPart}{apiKeyPart}";
        }
        #endregion
    }
}
