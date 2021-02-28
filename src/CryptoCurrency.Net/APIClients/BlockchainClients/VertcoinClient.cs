﻿using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using RestClient.Net.Abstractions;
// ReSharper disable UnusedMember.Global

namespace CryptoCurrency.Net.APIClients
{
    public class VertcoinClient : SomeClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new CurrencyCapabilityCollection { CurrencySymbol.VertCoin };
        #endregion

        #region Protected Properties
        protected override string BaseUriPath => "http://explorer.vertcoin.info";
        #endregion

        #region Constructor
        public VertcoinClient(CurrencySymbol currency, Func<Uri, IClient> restClientFactory) : base(currency, restClientFactory)
        {
        }
        #endregion
    }
}
