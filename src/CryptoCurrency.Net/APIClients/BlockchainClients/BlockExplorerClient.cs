using System;
using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients
{
    /// <summary>
    /// TODO: Make this client support other currencies...
    /// </summary>
    public class BlockExplorerClient : InsightClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection
        {
            CurrencySymbol.BitcoinCash,
            CurrencySymbol.Bitcoin,
            CurrencySymbol.ZCash
        };
        #endregion

        #region Protected Properties
        protected override string BaseUriPath
        {
            get
            {
                const string retval = "blockexplorer.com";

                switch(Currency.Name)
                {
                    case CurrencySymbol.BitcoinCashSymbolName:
                        return $"https://bitcoincash.{retval}";
                    case CurrencySymbol.BitcoinSymbolName:
                        return $"https://{retval}";
                    case CurrencySymbol.ZCashSymbolName:
                        return $"https://zcash.{retval}";
                }

                throw new NotImplementedException();
            }
        }

        protected override string AddressQueryStringBase => "/api/addr/";
        #endregion

        #region Constructor
        public BlockExplorerClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
        }
        #endregion
    }
}
