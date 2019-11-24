using CryptoCurrency.Net.Base.Model;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients.BlockchainClients
{
    public class GetTransactionsAtAddressArgs
    {
        private readonly RestClient rESTClient;
        private readonly string address;
        private readonly CurrencySymbol currency;
        private readonly BlockchainClientBase blockchainClientBase;

        public GetTransactionsAtAddressArgs(RestClient rESTClient, string address, CurrencySymbol currency, BlockchainClientBase blockchainClientBase)
        {
            this.rESTClient = rESTClient;
            this.address = address;
            this.currency = currency;
            this.blockchainClientBase = blockchainClientBase;
        }
    }
}