namespace CryptoCurrency.Net.AddressManagement
{
    public class PathResult
    {
        public string PublicKey { get; }
        public string Address { get; }

        public PathResult(string publicKey, string address)
        {
            PublicKey = publicKey;
            Address = address;
        }
    }
}
