using System;
using RestClientDotNet; using RestClientDotNet.Abstractions;

namespace CryptoCurrency.Net.UnitTests
{
    public class RESTClientFactory : IRestClientFactory
    {
        public IRestClient CreateRestClient(Uri baseUri)
        {
            return new RestClient(new NewtonsoftSerializationAdapter(), baseUri, new TimeSpan(0, 0, 3));
        }
    }
}
