using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using RestClientDotNet;
using System;

namespace CryptoCurrency.Net.APIClients
{

    /// <summary>
    /// TODO: Decommissioned because we can't be sure that it is not returning null for real addresses
    /// </summary>
    public class EthereumCommonwealthJSONRPCClient : JSONRPCClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.EthereumClassic };
        #endregion

        #region Public Properties
        protected override Uri BaseUriPath => new Uri("https://etc-geth.0xinfra.com");
        #endregion

        #region Constructor
        public EthereumCommonwealthJSONRPCClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
        }
        #endregion
    }
}
