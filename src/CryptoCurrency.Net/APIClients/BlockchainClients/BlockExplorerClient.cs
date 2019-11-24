using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using RestClientDotNet;
using System;

namespace CryptoCurrency.Net.APIClients
{
    /// <summary>
    /// TODO: Make this client support other currencies...
    /// Transaction Example: https://blockexplorer.com/api/tx/5756ff16e2b9f881cd15b8a7e478b4899965f87f553b6210d0f8e5bf5be7df1d
    /// Get Address with Transactions Sample: https://blockexplorer.com/api/addr/1Nh7uHdvY6fNwtQtM1G5EZAFPLC33B59rB
    /// </summary>
    public class BlockExplorerClient : InsightClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection
        {
            //BCH Seems to be retired after the split
            //CurrencySymbol.BitcoinCash,
            CurrencySymbol.Bitcoin,
            CurrencySymbol.ZCash
        };
        #endregion

        #region Protected Properties
        protected override Uri BaseUriPath
        {
            get
            {
                const string retval = "blockexplorer.com";

                switch (Currency.Name)
                {
                    case CurrencySymbol.BitcoinCashSymbolName:
                        return new Uri($"https://bitcoincash.{retval}");
                    case CurrencySymbol.BitcoinSymbolName:
                        return new Uri($"https://{retval}");
                    case CurrencySymbol.ZCashSymbolName:
                        return new Uri($"https://zcash.{retval}");
                    default:
                        return null;
                }
            }
        }

        protected override string AddressQueryStringBase => "/api/addr/";
        protected override string TransactionQueryStringBase => "/api/tx/";
        #endregion

        #region Constructor
        public BlockExplorerClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
        }
        #endregion
    }
}
