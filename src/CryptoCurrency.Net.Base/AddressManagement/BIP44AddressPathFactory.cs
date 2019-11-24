using CryptoCurrency.Net.Base.Abstractions.AddressManagement;

namespace CryptoCurrency.Net.AddressManagement
{
    public class BIP44AddressPathFactory : IAddressPathFactory
    {
        public bool IsSegwit { get; }
        public uint CointType { get; }

        public BIP44AddressPathFactory(bool isSegwit, uint coinType)
        {
            IsSegwit = isSegwit;
            CointType = coinType;
        }

        public IAddressPath GetAddressPath(uint change, uint account, uint addressIndex)
        {
            return new BIP44AddressPath(IsSegwit, CointType, account, change == 0 ? false : true, addressIndex);
        }
    }
}
