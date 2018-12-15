using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Insight;
using RestClientDotNet;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    public abstract class InsightClientBase : BlockchainClientBase
    {
        #region Constructor

        protected InsightClientBase(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            RESTClient = restClientFactory.CreateRESTClient(new Uri(BaseUriPath));
            Currency = currency;
        }
        #endregion

        #region Protected Overridable Properties
        protected abstract string BaseUriPath { get; }
        protected virtual string AddressQueryStringBase => "/insight-api/addr/";
        #endregion

        #region Private Methods
        private async Task<Address> GetInsightAddress(string address)
        {
            return await RESTClient.GetAsync<Address>($"{AddressQueryStringBase}{address}");
        }
        #endregion

        #region Func
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            var addressModel = await GetInsightAddress(address);
            var retVal = new BlockChainAddressInformation(address, addressModel.balance, addressModel.transactions.Count);
            return retVal;
        }

        public async override Task<TransactionsAtAddress> GetTransactionsAtAddress(string address)
        {
            var lastUpdate = DateTime.Now;
            var insightAddress = await GetInsightAddress(address);
            return new TransactionsAtAddress(address, insightAddress.transactions.Select(t => new Transaction(t))) {  LastUpdate = lastUpdate };
        }

        #endregion
    }
}
