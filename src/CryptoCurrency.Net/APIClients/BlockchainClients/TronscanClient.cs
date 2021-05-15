using CryptoCurrency.Net.APIClients.Model.Tronscan;
using CryptoCurrency.Net.Base.Model;
using Microsoft.Extensions.Logging;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Threading.Tasks;
using ts = CryptoCurrency.Net.APIClients.Model.Tronscan;

namespace CryptoCurrency.Net.APIClients.BlockchainClients
{
    public class TronscanClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.Tron };
        #endregion

        public TronscanClient(
            CurrencySymbol currency,
            CreateClient restClientFactory,
            ILogger<TronscanClient> logger) : base(currency, restClientFactory, logger)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = restClientFactory(GetType().Name, (o) => o.BaseUrl = new("https://apilist.tronscan.org"));
        }

        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            Account account = await RESTClient.GetAsync<Account>($"api/account?address={address}");

            ts.Transaction transactions = await RESTClient.GetAsync<ts.Transaction>($"api/transaction?sort=-timestamp&count=true&limit=1&start=0&address={address}");

            var balance = account.balance;
            return new BlockChainAddressInformation(account.address, balance * (decimal).000001, transactions.data.Count > 0);
        }
    }
}
