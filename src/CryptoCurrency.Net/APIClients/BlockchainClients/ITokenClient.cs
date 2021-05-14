using CryptoCurrency.Net.Base.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients.BlockchainClients
{
    public interface ITokenClient : IAPIClient
    {
        Task<IEnumerable<TokenBalance>> GetTokenBalances(IEnumerable<string> addresses);
    }
}