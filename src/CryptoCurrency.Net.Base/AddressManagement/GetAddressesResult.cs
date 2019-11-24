using System.Collections.ObjectModel;

namespace CryptoCurrency.Net.AddressManagement
{
    public class GetAddressesResult
    {
        public Collection<AccountResult> Accounts { get; } = new Collection<AccountResult>();
    }
}
