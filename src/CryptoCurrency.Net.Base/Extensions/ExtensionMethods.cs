using System.Collections.Generic;
using System.Linq;

namespace CryptoCurrency.Net.Base.Extensions
{
    public static class MyExtensions
    {
        public static string ToHexString(this IEnumerable<byte> bytes)
        {
            return bytes.Aggregate(string.Empty, (current, theByte) => current + theByte.ToString("X2"));
        }
    }
}
