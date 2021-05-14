using CryptoCurrency.Net.Base.Abstractions.AddressManagement;

namespace CryptoCurrency.Net.Base.AddressManagement
{
    public class AddressPathElement : IAddressPathElement
    {
        public uint Value { get; set; }

        public bool Harden { get; set; }
    }
}
