using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients
{
    public class BtgExplorerClient : InsightClientBase, IBlockchainClient
    {
        #region Constructor
        public BtgExplorerClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
        }
        #endregion

        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.BitcoinGold };
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Example: https://btgexplorer.com/api/addr/[Address]
        /// </summary>
        protected override string BaseUriPath => "https://btgexplorer.com";
        protected override string AddressQueryStringBase => "/api/addr/";
        #endregion
    }
}
