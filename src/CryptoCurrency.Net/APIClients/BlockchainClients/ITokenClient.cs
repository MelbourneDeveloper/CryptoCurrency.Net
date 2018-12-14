using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoCurrency.Net.Model;

namespace CryptoCurrency.Net.APIClients.BlockchainClients
{
    public interface ITokenClient : IAPIClient
    {
        Task<IEnumerable<TokenBalance>> GetTokenBalances(IEnumerable<string> addresses);
    }
}