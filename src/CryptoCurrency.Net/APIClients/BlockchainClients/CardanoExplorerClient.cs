﻿using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using RestClientDotNet;
using System;

namespace CryptoCurrency.Net.APIClients
{
    /// <summary>
    /// https://cardanodocs.com/technical/explorer/api/
    /// </summary>
    public class CardanoExplorerClient : CardanoExplorerBase, IBlockchainClient
    {
        #region Public Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.Cardano };
        #endregion

        public CardanoExplorerClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
        }

        public override Uri BaseAddress => new Uri("https://cardanoexplorer.com");
    }
}
