﻿using System;

namespace CryptoCurrency.Net.Base.AddressManagement
{
    public static class AddressUtilities
    {
        private const uint HardeningConstant = 0x80000000;

        public static uint HardenNumber(uint softNumber) => (softNumber | HardeningConstant) >> 0;

        public static uint UnhardenNumber(uint hardNumber) => hardNumber ^ HardeningConstant;

        public static string[] Split(this string path, char splitter)
        {
            return path == null
                ? throw new ArgumentNullException(nameof(path))
                : path.Split(new[] { splitter }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
