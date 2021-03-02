using CryptoCurrency.Net.Base.Model;
using RestClient.Net.Abstractions;

namespace CryptoCurrency.Net.APIClients.BlockchainClients
{
    public class GetTransactionsAtAddressArgs
    {
        private readonly RestClient rESTClient;
        private readonly string address;
        private readonly CurrencySymbol currency;
        private readonly BlockchainClientBase blockchainClientBase;

        public GetTransactionsAtAddressArgs(IClient client, string address, CurrencySymbol currency, BlockchainClientBase blockchainClientBase)
        {
            RESTClient = client;
            this.address = address;
            this.currency = currency;
            this.blockchainClientBase = blockchainClientBase;
        }
    }
}