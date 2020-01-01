using System.Collections.Generic;
using CryptoCurrency.Net.Model;
using RestClient.Net;
namespace CryptoCurrency.Net.APIClients.BlockchainClients.CallArguments
{
    public class GetAddressesArgs : CallArgs
    {
        public GetAddressesArgs(Client client, IEnumerable<string> addresses, CurrencySymbol currencySymbol, BlockchainClientBase blockChainClientBase) : base(client)
        {
            Addresses = addresses;
            CurrencySymbol = currencySymbol;
            Client = blockChainClientBase;
        }

        public IEnumerable<string> Addresses { get; }
        public CurrencySymbol CurrencySymbol { get; }
        public BlockchainClientBase Client { get; }
    }
}
