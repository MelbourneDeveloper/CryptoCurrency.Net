using RestClient.Net.Abstractions;

namespace CryptoCurrency.Net.APIClients.BlockchainClients.CallArguments
{
    public class CallArgs
    {
        public IClient RESTClient { get; set; }

        internal CallArgs(IClient client) => RESTClient = client;
    }
}
