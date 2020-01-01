using System;
using RestClient.Net; using RestClient.Net.Abstractions;

namespace CryptoCurrency.Net.UnitTests
{
    public class RESTClientFactory : IClientFactory
    {
        public IRestClient CreateRestClient(Uri baseUri)
        {
            return new RestClient(new NewtonsoftSerializationAdapter(), baseUri, new TimeSpan(0, 0, 3));
        }

        public IRestClient CreateRestClient()
        {
            return new RestClient(new NewtonsoftSerializationAdapter(), null, new TimeSpan(0, 0, 3));
        }
    }
}
