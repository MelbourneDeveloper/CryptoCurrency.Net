using CryptoCurrency.Net.Base.Abstractions.AddressManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.Base.AddressManagement
{
    public class AddressManager
    {
        #region Public Properties
        public IAddressDeriver HardwarewalletManager { get; }
        public IAddressPathFactory AddressPathFactory { get; }
#pragma warning disable IDE0052 // Remove unread private members
        //I don't know why these fields are here
        private uint Purpose { get; }
        private uint CoinType { get; }
#pragma warning restore IDE0052 // Remove unread private members
        #endregion

        #region Constructor

        public AddressManager(IAddressDeriver hardwarewalletManager, IAddressPathFactory addressPathFactory)
        {
            HardwarewalletManager = hardwarewalletManager;
            AddressPathFactory = addressPathFactory;
        }

        public AddressManager(IAddressDeriver hardwarewalletManager, IAddressPathFactory addressPathFactory, bool isSegit, uint cointType) : this(hardwarewalletManager, addressPathFactory)
        {
            Purpose = isSegit ? (uint)49 : 44;
            CoinType = cointType;
        }
        #endregion

        #region Private Methods
        private async Task<PathResult> GetPathResult(bool includePublicKeys, uint account, uint index, bool isChange)
        {
            var addressPath = AddressPathFactory.GetAddressPath((uint)(isChange ? 1 : 0), account, index);

            var address = await HardwarewalletManager.GetAddressAsync(addressPath, false, false);

            string publicKey = null;
            if (includePublicKeys)
            {
                publicKey = await HardwarewalletManager.GetAddressAsync(addressPath, true, false);
            }

            return new PathResult(publicKey, address);
        }
        #endregion

        #region Public Methods
        public async Task<GetAddressesResult> GetAddressesAsync(uint startIndex, int numberOfAddresses, int numberOfAccounts, bool includeChangeAddresses, bool includePublicKeys)
        {
            var retVal = new GetAddressesResult();

            //Iterate through accounts
            for (uint account = 0; account < numberOfAccounts; account++)
            {
                var addresses = new List<PathResult>();
                List<PathResult> changeAddresses = null;
                if (includeChangeAddresses)
                {
                    changeAddresses = new List<PathResult>();
                }

                //Create the result for the account
                var accountResult = new AccountResult(account, addresses, changeAddresses);

                retVal.Accounts.Add(accountResult);

                //Iterate through address indexes
                for (var i = 0; i < numberOfAddresses; i++)
                {
                    var index = startIndex + (uint)i;

                    accountResult.Addresses.Add(await GetPathResult(includePublicKeys, account, index, false));

                    if (includeChangeAddresses)
                    {
                        accountResult.ChangeAddresses.Add(await GetPathResult(includePublicKeys, account, index, true));
                    }
                }
            }

            return retVal;
        }
        #endregion
    }
}
