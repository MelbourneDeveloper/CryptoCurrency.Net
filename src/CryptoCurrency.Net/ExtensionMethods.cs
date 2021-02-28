using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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


        /// <summary>
        /// Ensures that we don't call the API more than the specified number of times per second
        /// </summary>
        public static async Task<T> GetAsync<T>(this IClient restClient, SemaphoreSlim semaphore, IList<DateTime> calls, string queryString, int maxCallsPerSecond = 5)
        {
            try
            {
                await semaphore.WaitAsync();


                if (calls.Where(d => d > DateTime.Now.AddSeconds(-1)).Count() >= maxCallsPerSecond)
                {
                    await Task.Delay(1000);
                    calls.Clear();
                }

                calls.Add(DateTime.Now);

                return await restClient.GetAsync<T>(new Uri(queryString, UriKind.Relative));
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
