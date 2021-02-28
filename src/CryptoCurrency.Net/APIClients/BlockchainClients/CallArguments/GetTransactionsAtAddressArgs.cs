using CryptoCurrency.Net.Model;
using RestClient.Net.Abstractions;

namespace CryptoCurrency.Net.APIClients.BlockchainClients
{
    public class GetTransactionsAtAddressArgs
    {
#pragma warning disable IDE0052 
        private readonly IClient RESTClient;
        private readonly string address;
        private readonly CurrencySymbol currency;
        private readonly BlockchainClientBase blockchainClientBase;
#pragma warning restore IDE0052 

        public GetTransactionsAtAddressArgs(IClient client, string address, CurrencySymbol currency, BlockchainClientBase blockchainClientBase)
        {
            RESTClient = client;
            this.address = address;
            this.currency = currency;
            this.blockchainClientBase = blockchainClientBase;
        }
    }
}