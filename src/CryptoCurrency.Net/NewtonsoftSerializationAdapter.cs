using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestClientDotNet
{
    public class NewtonsoftSerializationAdapter : RestClientSerializationAdapterBase, ISerializationAdapter
    {
        #region Implementation
        public async Task<T> DeserializeAsync<T>(byte[] binary)
        {
            var retVal = await DeserializeAsync(binary, typeof(T));
            return (T)retVal;
        }

        public async Task<object> DeserializeAsync(byte[] data, Type type)
        {
            string markup = null;
            try
            {
                markup = GetMarkup(data);

                if (type == typeof(string))
                {
                    return markup;
                }

                var retVal = await Task.Run(() => JsonConvert.DeserializeObject(markup, type));

                return retVal;
            }
            catch (Exception ex)
            {
                throw new DeserializationException(data, markup, ex);
            }
        }

        public async Task<byte[]> SerializeAsync<T>(T value)
        {
            var json = await Task.Run(() => JsonConvert.SerializeObject(value));
            var binary = await Task.Run(() => Encoding.GetBytes(json));
            return binary;
        }
        #endregion
    }
}
