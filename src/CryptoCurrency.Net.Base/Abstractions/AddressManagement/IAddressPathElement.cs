namespace CryptoCurrency.Net.Base.Abstractions.AddressManagement
{
    public interface IAddressPathElement
    {
        uint Value { get; }
        bool Harden { get; }
    }
}
