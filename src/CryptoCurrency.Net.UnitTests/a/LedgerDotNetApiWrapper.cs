using CryptoCurrency.Net.Abstractions.AddressManagement;
using System;
using System.Threading.Tasks;

namespace Hardwarewallets.Net
{
    public class LedgerDotNetApiWrapper : IAddressDeriver
    {
        #region Fields
        private readonly LedgerClient _LedgerClient;
        #endregion

        #region Constructor
        public LedgerDotNetApiWrapper(LedgerClient ledgerClient)
        {
            _LedgerClient = ledgerClient;
        }
        #endregion

        #region Public Methods
        public async Task<string> GetAddressAsync(IAddressPath addressPath, bool isPublicKey, bool display)
        {
            var response = await GetPublicKey(addressPath, display);
            return isPublicKey ? response.UncompressedPublicKey.ToHex() : response.Address;
        }
        #endregion

        #region Private Methods
        private async Task<GetWalletPubKeyResponse> GetPublicKey(IAddressPath addressPath, bool display)
        {
            throw new NotImplementedException();
            //if (addressPath.CoinType == 0)
            //{
            //}
            //else
            //{
            //    throw new NotImplementedException();
            //}

            //var response = await _LedgerClient.GetWalletPubKeyAsync(new KeyPath($"{addressPath.Purpose}'/{addressPath.CoinType}'/{addressPath.Account}'/{addressPath.Change}/{addressPath.AddressIndex}"), LedgerClient.AddressType.Legacy, display);
            //return response;
        }
        #endregion
    }
}
