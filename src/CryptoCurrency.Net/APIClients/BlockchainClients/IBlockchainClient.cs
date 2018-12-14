using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoCurrency.Net.Model;

namespace CryptoCurrency.Net.APIClients.BlockchainClients
{
    public interface IBlockchainClient : IAPIClient
    {
        Task<IEnumerable<BlockChainAddressInformation>> GetAddresses(IEnumerable<string> addresses);
    }
}