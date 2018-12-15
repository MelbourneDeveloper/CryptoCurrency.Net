using CryptoCurrency.Net.Model;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients.BlockchainClients
{
    public class GetTransactionsAtAddressArgs
    {
        private RestClient rESTClient;
        private string address;
        private CurrencySymbol currency;
        private BlockchainClientBase blockchainClientBase;

        public GetTransactionsAtAddressArgs(RestClient rESTClient, string address, CurrencySymbol currency, BlockchainClientBase blockchainClientBase)
        {
            this.rESTClient = rESTClient;
            this.address = address;
            this.currency = currency;
            this.blockchainClientBase = blockchainClientBase;
        }
    }
}