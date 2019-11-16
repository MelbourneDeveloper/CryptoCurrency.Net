using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients
{
    /// <summary>
    ///https://blockchain.info/multiaddr?active=$address|$address
    /// </summary>
    public class BlockchainClient : SomeClient2Base, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new CurrencyCapabilityCollection { CurrencySymbol.Bitcoin };
        #endregion

        #region Protected Override Properties
        protected override string BaseUriPath => "https://blockchain.info";
        #endregion

        #region Constructor
        public BlockchainClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
        }
        #endregion

        #region Protected Overrides
        protected override string GetQueryString(string addressesPart)
        {
            return $"/multiaddr?active={addressesPart}";
        }
        #endregion
    }
}
