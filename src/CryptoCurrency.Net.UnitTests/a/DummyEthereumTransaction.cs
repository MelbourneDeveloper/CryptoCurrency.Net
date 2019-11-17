using CryptoCurrency.Net.Abstractions.AddressManagement;
using System.Numerics;

namespace Hardwarewallets.Net.UnitTests
{
    public class DummyEthereumTransaction : IEthereumTransaction
    {
        public uint ChainId => 0;

        public BigInteger GasPrice => 1000000000;

        public BigInteger GasLimit => 21000;

        public BigInteger Nonce => 0;

        public IAddressPath From => new AddressPath(false, 60, 0, false, 0);

        public decimal Value => (decimal)2.03016804;

        public string To => "0x3f2dD9850509367b57C900F7e1C5f4F0bfF1014B";

        public byte[] Data => new byte[0];
    }
}
