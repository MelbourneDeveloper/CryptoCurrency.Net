using System;
using RestClientDotNet;

namespace CryptoCurrency.Net.UnitTests
{
    public class RESTClientFactory : IRestClientFactory
    {
        public RestClient CreateRESTClient(Uri baseUri)
        {
            return new RestClient(new NewtonsoftSerializationAdapter(), baseUri);
        }
    }
}
