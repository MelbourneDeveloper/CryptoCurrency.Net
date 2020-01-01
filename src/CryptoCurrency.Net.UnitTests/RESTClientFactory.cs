using System;
using RestClient.Net;
using RestClient.Net.Abstractions;

namespace CryptoCurrency.Net.UnitTests
{
    public class RESTClientFactory : IClientFactory
    {
        public IClient CreateClient(string name)
        {
            return new Client(new NewtonsoftSerializationAdapter());
        }
    }
}
