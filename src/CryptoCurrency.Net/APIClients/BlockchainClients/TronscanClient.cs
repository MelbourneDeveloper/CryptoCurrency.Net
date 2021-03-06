﻿using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Tronscan;
using RestClientDotNet;
using System;
using System.Threading.Tasks;
using ts = CryptoCurrency.Net.Model.Tronscan;

namespace CryptoCurrency.Net.APIClients.BlockchainClients
{
    public class TronscanClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.Tron };
        #endregion

        public TronscanClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            RESTClient = new RestClient(new NewtonsoftSerializationAdapter(), new Uri("https://apilist.tronscan.org"));
        }

        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            var account = await RESTClient.GetAsync<Account>($"api/account?address={address}");

            var transactions = await RESTClient.GetAsync<ts.Transaction>($"api/transaction?sort=-timestamp&count=true&limit=1&start=0&address={address}");

            var balance = account.balance;
            return new BlockChainAddressInformation(account.address, balance * (decimal).000001, transactions.data.Count > 0);
        }
    }
}
