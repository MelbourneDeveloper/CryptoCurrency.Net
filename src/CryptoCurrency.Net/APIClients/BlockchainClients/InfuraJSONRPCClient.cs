using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using Microsoft.Extensions.Logging;
using RestClient.Net.Abstractions;
using System;

namespace CryptoCurrency.Net.APIClients
{

    /// <summary>
    /// TODO: Decommissioned because we can't be sure that it is not returning null for real addresses
    /// </summary>
    public class InfuraJSONRPCClient : JSONRPCClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.Ethereum };
        #endregion

        #region Public Properties
        protected override Uri BaseUriPath => new("https://mainnet.infura.io");
        #endregion

        #region Constructor
        public InfuraJSONRPCClient(CurrencySymbol currency, Func<Uri, IClient> restClientFactory, ILogger<InfuraJSONRPCClient> logger) : base(currency, restClientFactory, logger)
        {
        }
        #endregion
    }
}
