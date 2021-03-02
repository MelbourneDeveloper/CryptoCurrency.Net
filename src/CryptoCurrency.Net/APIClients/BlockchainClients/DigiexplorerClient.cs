﻿using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Base.Model;
using RestClient.Net.Abstractions;
using System;

namespace CryptoCurrency.Net.APIClients
{
    /// <summary>
    /// TODO: Make this client support other currencies...
    /// </summary>
    public class DigiexplorerClient : InsightClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.DigiByte };
        #endregion

        #region Protected Properties
        protected override Uri BaseUriPath => new Uri("https://digiexplorer.info/");

        protected override string AddressQueryStringBase => "/api/addr/";
        #endregion

        #region Constructor
        public DigiexplorerClient(CurrencySymbol currency, Func<Uri, IClient> restClientFactory) : base(currency, restClientFactory)
        {
        }
        #endregion
    }
}
