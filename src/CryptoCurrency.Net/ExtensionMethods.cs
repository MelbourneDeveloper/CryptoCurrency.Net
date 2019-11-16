using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CryptoCurrency.Net
{
    public static class MyExtensions
    {
        public static string ToHexString(this IEnumerable<byte> bytes)
        {
            return bytes.Aggregate(string.Empty, (current, theByte) => current + theByte.ToString("X2"));
        }

        /// <summary>
        /// TODO: This looks inefficient. It's also not thread safe.
        /// </summary>
        public static ObservableCollection<T> Concatenate<T>(this ObservableCollection<T> list, ObservableCollection<T> list2)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (list2 == null) throw new ArgumentNullException(nameof(list2));

            var retVal = new ObservableCollection<T>();

            foreach (var item in list)
            {
                retVal.Add(item);
            }

            foreach (var item in list2)
            {
                retVal.Add(item);
            }

            return retVal;
        }

    }
}
