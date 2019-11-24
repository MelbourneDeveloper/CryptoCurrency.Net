namespace CryptoCurrency.Net.Abstractions.AddressManagement
{
    public interface IAddressPathElement
    {
        uint Value { get; }
        bool Harden { get; }
    }
}
