using CryptoCurrency.Net.Base.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients.BlockchainClients
{
    public interface IBlockchainClient : IAPIClient
    {
        Task<IEnumerable<BlockChainAddressInformation>> GetAddresses(IEnumerable<string> addresses);
        Task<TransactionsAtAddress> GetTransactionsAtAddress(string address);
    }
}