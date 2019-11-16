namespace CryptoCurrency.Net.Abstractions.AddressManagement
{
    public interface IAddressPathFactory
    {
        IAddressPath GetAddressPath(uint change, uint account, uint addressIndex);
    }
}
