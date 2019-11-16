using CryptoCurrency.Net.Abstractions.AddressManagement;

namespace CryptoCurrency.Net.AddressManagement
{
    public class AddressPathElement : IAddressPathElement
    {
        public uint Value { get; set; }

        public bool Harden { get; set; }
    }
}
