namespace Hardwarewallets.Net.UnitTests
{
    public class DummySignedEthereumTransaction : ISignedEthereumTransaction
    {
        public byte[] SignatureR { get; set; } = new byte[32];
        public byte[] SignatureS { get; set; } = new byte[32];
        public byte[] SignatureV { get; set; } = new byte[32];
    }
}
