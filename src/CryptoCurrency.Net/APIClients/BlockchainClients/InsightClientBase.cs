using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using RestClientDotNet;
using System;
using System.Linq;
using System.Threading.Tasks;
using insight = CryptoCurrency.Net.Model.Insight;

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
        protected virtual string TransactionQueryStringBase => "/insight-api/tx/";
        #endregion

        #region Private Methods
        private async Task<insight.Address> GetInsightAddress(string address)
        {
            return await RESTClient.GetAsync<insight.Address>($"{AddressQueryStringBase}{address}");
        }
        #endregion

        #region Func
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            var addressModel = await GetInsightAddress(address);
            var retVal = new BlockChainAddressInformation(address, addressModel.balance, addressModel.transactions.Count);
            return retVal;
        }

        /// <summary>
        /// TODO: This shouldn't directly override the method. There should be a func instead.
        /// </summary>
        public override async Task<TransactionsAtAddress> GetTransactionsAtAddress(string address)
        {
            var lastUpdate = DateTime.Now;
            var insightAddress = await GetInsightAddress(address);
            var returnValue = new TransactionsAtAddress(address, insightAddress.transactions.Select(t => new Transaction(t))) { LastUpdate = lastUpdate };

            foreach (var transaction in returnValue.Transactions)
            {
                var insightTransaction = await RESTClient.GetAsync<insight.Transaction>($"{TransactionQueryStringBase}{transaction.TransactionId}");
                transaction.TransactionId = insightTransaction.txid;
                foreach (var vin in insightTransaction.vin)
                {
                    transaction.Inputs.Add(new TransactionPiece { Amount = vin.value, Address = vin.addr });
                }

                foreach (var vout in insightTransaction.vout)
                {
                    transaction.Outputs.Add(new TransactionPiece
                    {
                        Amount = vout.value,
                        //TODO: Not sure if this logic is correct...
                        Address = vout.scriptPubKey.addresses.FirstOrDefault()
                    });
                }
            }

            return returnValue;
        }

        #endregion
    }
}
