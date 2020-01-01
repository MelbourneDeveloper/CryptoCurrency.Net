using RestClient.Net;
namespace CryptoCurrency.Net.APIClients.BlockchainClients.CallArguments
{
    public class CallArgs
    {
        public Client RESTClient { get; set; }

        internal CallArgs(Client client)
        {
            RESTClient = client;
        }
    }
}
