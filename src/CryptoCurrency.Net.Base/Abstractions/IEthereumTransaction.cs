using System.Numerics;

namespace Hardwarewallets.Net.Model
{
    public interface IEthereumTransaction : ITransaction
    {
        uint ChainId { get; }
        BigInteger GasPrice { get; }
        BigInteger GasLimit { get; }
        BigInteger Nonce { get; }
        byte[] Data { get; }
    }
}
