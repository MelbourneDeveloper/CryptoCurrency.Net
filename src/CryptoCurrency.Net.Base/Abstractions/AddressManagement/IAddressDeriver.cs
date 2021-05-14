﻿using System.Threading.Tasks;

namespace CryptoCurrency.Net.Base.Abstractions.AddressManagement
{
    public interface IAddressDeriver
    {
        Task<string> GetAddressAsync(IAddressPath addressPath, bool isPublicKey, bool display);
    }
}
