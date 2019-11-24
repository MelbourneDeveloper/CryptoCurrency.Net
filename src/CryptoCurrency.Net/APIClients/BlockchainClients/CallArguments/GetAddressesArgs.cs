using System.Collections.Generic;
using CryptoCurrency.Net.Base.Model;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients.BlockchainClients.CallArguments
{
    public class GetAddressesArgs : CallArgs
    {
        public GetAddressesArgs(RestClient restClient, IEnumerable<string> addresses, CurrencySymbol currencySymbol, BlockchainClientBase client) : base(restClient)
        {
            Addresses = addresses;
            CurrencySymbol = currencySymbol;
            Client = client;
        }

        public IEnumerable<string> Addresses { get; }
        public CurrencySymbol CurrencySymbol { get; }
        public BlockchainClientBase Client { get; }
    }
}
