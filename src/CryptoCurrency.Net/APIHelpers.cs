using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
namespace CryptoCurrency.Net.Helpers
{
    public static class APIHelpers
    {
        #region Enums
        public enum HashAlgorithmType
        {
            HMACEightBit,
            HMACNineBit,
            HMACThreeEightFour,
        }
        #endregion

        #region Private Static Fields
        //TODO: This is not using the factory interface...
        //private static readonly Client GetDateRESTClient = new Client(new NewtonsoftSerializationAdapter(), new Uri("http://www.convert-unix-time.com"));
        private static readonly DateTime EpochDate = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        #endregion

        #region Private Static Methods
        private static HashAlgorithm GetHashAlgorithm(HashAlgorithmType hashAlgorithmType)
        {
            switch (hashAlgorithmType)
            {
                case HashAlgorithmType.HMACEightBit:
                    return new HMACSHA256();
                case HashAlgorithmType.HMACNineBit:
                    return new HMACSHA512();
                case HashAlgorithmType.HMACThreeEightFour:
                    return new HMACSHA384();
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion

        #region Public Static Methods

        #region Misc
        public static void DebugWriteLine(string message, DateTime startTime) => Debug.WriteLine($"Time: {DateTime.Now:hh:mm:ss}. Message: {message} in {(DateTime.Now - startTime).TotalMilliseconds} milliseconds.");
        #endregion

        #region Hashing
        //TODO: Consolidate these two methods ....
        public static string GetHash(string message, string key, HashAlgorithmType hashAlgorithmType, Encoding encoding)
        {
            var bytes = GetHashAsBytes(message, key, hashAlgorithmType, encoding);
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        public static byte[] GetHashAsBytes(string message, string key, HashAlgorithmType hashAlgorithmType, Encoding encoding)
        {
            if (encoding == null) throw new ArgumentNullException(nameof(encoding));

            var messageBytes = encoding.GetBytes(message);
            var keyBytes = encoding.GetBytes(key);
            return GetHashAsBytes(messageBytes, keyBytes, hashAlgorithmType);
        }

        private static byte[] GetHashAsBytes(byte[] messageBytes, byte[] keyBytes, HashAlgorithmType hashAlgorithmType)
        {

            using (var hashAlgorithm = GetHashAlgorithm(hashAlgorithmType))
            {
                if (hashAlgorithm is HMAC hmac)
                {
                    hmac.Key = keyBytes;
                }

                return hashAlgorithm.ComputeHash(messageBytes);
            }
        }

        public static string GetSignature(Uri baseUri, string url, IDictionary<string, object> requestParameters, string apiSecret, HashAlgorithmType hmacshaType)
        {
            if (requestParameters == null) throw new ArgumentNullException(nameof(requestParameters));

            var input = new StringBuilder(new Uri(baseUri, url).ToString());

            foreach (var key in requestParameters.Keys)
            {
                input.Append(',');
                var value = requestParameters[key];

                var resultValue = value != null && value.GetType().IsArray
                    ? !(value is string[] array) || !array.Any() ? string.Empty : string.Join(",", array.Select(tt => $"{tt}"))
                    : value is string s ? s.Replace("\r", "").Replace("\n", "") : value;

                input.AppendFormat(CultureInfo.InvariantCulture, "{0}={1}", key, resultValue);
            }

            return GetHash(input.ToString(), apiSecret, hmacshaType, Encoding.ASCII);
        }
        #endregion

        #region Time

        /// <summary>
        /// Warning: Not an accurate timestamp. Only useful as a Nonce
        /// </summary>
        /// <returns></returns>
        public static string GetNonce() => DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);

        //public static async Task<DateTime> GetCurrentDateTimeFromConvertUnixTimeAsync()
        //{
        //    CurrentTime currentTimeModel = await GetDateRESTClient.GetAsync<CurrentTime>("api?timestamp=now");
        //    return GetDateTimeFromSecondsSinceEpoch(currentTimeModel.timestamp);
        //}

        //public static async Task<long> GetUnixTimeStampFromConvertUnixTimeAsync()
        //{
        //    var currentDateTime = await GetCurrentDateTimeFromConvertUnixTimeAsync();
        //    return GetUnixTimestamp(currentDateTime);
        //}

        public static DateTime GetDateTimeFromSecondsSinceEpoch(long seconds) => EpochDate.AddSeconds(seconds);

        public static long GetCurrentUnixTimestamp() => GetUnixTimestamp(DateTime.UtcNow);

        public static long GetUnixTimestamp(DateTime dateTime) => (long)(dateTime - EpochDate).TotalMilliseconds;
        #endregion

        #endregion
    }
}
