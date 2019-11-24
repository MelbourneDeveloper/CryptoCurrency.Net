namespace CryptoCurrency.Net.Base.Abstractions.AddressManagement
{
    public interface IAddressPathFactory
    {
        IAddressPath GetAddressPath(uint change, uint account, uint addressIndex);
    }
}
