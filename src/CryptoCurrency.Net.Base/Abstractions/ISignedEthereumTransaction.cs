namespace Hardwarewallets.Net.Model
{
    public interface ISignedEthereumTransaction : ISignedTransaction
    {
        byte[] SignatureR { get; set; }
        byte[] SignatureS { get; set; }
        byte[] SignatureV { get; set; }
    }
}
