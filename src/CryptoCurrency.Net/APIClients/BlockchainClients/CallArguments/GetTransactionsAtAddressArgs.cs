using CryptoCurrency.Net.Model;
using RestClient.Net;
namespace CryptoCurrency.Net.APIClients.BlockchainClients
{
    public class GetTransactionsAtAddressArgs
    {
        private readonly Client RESTClient;
        private readonly string address;
        private CurrencySymbol currency;
        private readonly BlockchainClientBase blockchainClientBase;

        public GetTransactionsAtAddressArgs(Client client, string address, CurrencySymbol currency, BlockchainClientBase blockchainClientBase)
        {
            RESTClient = client;
            this.address = address;
            this.currency = currency;
            this.blockchainClientBase = blockchainClientBase;
        }
    }
}