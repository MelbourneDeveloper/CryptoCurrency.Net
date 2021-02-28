﻿using CryptoCurrency.Net.Abstractions.AddressManagement;
using System;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.UnitTests
{
    public class DummyAddressDeriver : IAddressDeriver
    {
        public Task<string> GetAddressAsync(IAddressPath addressPath, bool isPublicKey, bool display)
        {
            if (addressPath is IBIP44AddressPath bip44AddressPath)
            {
                if (isPublicKey)
                {
                    switch (bip44AddressPath.CoinType)
                    {
                        case 0:
                            return Task.FromResult("02a1633cafcc01ebfb6d78e39f687a1f0995c62fc95f51ead10a02ee0be551b5dc");
                        case 60:
                            return Task.FromResult("0x3f2dD9850509367b57C900F7e1C5f4F0bfF1014Bf4F0bfF1014B");
                        default:
                            throw new NotImplementedException();
                    }
                }
                else
                {
                    switch (bip44AddressPath.CoinType)
                    {
                        case 0:
                            return Task.FromResult("02a1633cafcc01ebfb6d78e39f687a1f0995c62fc95f51ead10a02ee0be551b5dc");
                        case 60:
                            return Task.FromResult("0x3f2dD9850509367b57C900F7e1C5f4F0bfF1014B");
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

    }
}
