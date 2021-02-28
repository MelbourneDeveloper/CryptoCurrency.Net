using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Tronscan;
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

        public TronscanClient(CurrencySymbol currency, Func<Uri, IClient> restClientFactory) : base(currency, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            var baseUri = new Uri("https://apilist.tronscan.org");
            RESTClient = RESTClientFactory(baseUri);
            RESTClient.BaseUri = baseUri;
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
