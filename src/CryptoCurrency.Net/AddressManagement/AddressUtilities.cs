using System;

namespace CryptoCurrency.Net.AddressManagement
{
    public static class AddressUtilities
    {
        private const uint HardeningConstant = 0x80000000;

        public static uint HardenNumber(uint softNumber)
        {
            return (softNumber | HardeningConstant) >> 0;
        }

        public static uint UnhardenNumber(uint hardNumber)
        {
            return hardNumber ^ HardeningConstant;
        }

        public static string[] Split(this string path, char splitter)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            return path.Split(new[] { splitter }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
