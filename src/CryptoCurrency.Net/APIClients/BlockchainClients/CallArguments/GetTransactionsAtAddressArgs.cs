using CryptoCurrency.Net.Base.Model;
using RestClient.Net.Abstractions;

namespace CryptoCurrency.Net.APIClients.BlockchainClients
{
    public class GetTransactionsAtAddressArgs
    {
        public IClient RESTClient { get; }
        public string Address { get; }
        public CurrencySymbol Currency { get; }
        public BlockchainClientBase BlockchainClientBase { get; }

        public GetTransactionsAtAddressArgs(IClient client, string address, CurrencySymbol currency, BlockchainClientBase blockchainClientBase)
        {
            RESTClient = client;
            Address = address;
            Currency = currency;
            BlockchainClientBase = blockchainClientBase;
        }
    }
}