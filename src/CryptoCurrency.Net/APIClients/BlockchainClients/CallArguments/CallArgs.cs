using RestClient.Net;
namespace CryptoCurrency.Net.APIClients.BlockchainClients.CallArguments
{
    public class CallArgs
    {
        public RestClient RESTClient { get; set; }

        internal CallArgs(RestClient rESTClient)
        {
            RESTClient = rESTClient;
        }
    }
}
