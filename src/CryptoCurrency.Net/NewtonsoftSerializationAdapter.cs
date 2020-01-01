using Newtonsoft.Json;
using RestClientDotNet.Abstractions;
using System.Text;

namespace RestClientDotNet
{
    public class NewtonsoftSerializationAdapter : ISerializationAdapter
    {
        #region Implementation
        public TResponseBody Deserialize<TResponseBody>(byte[] data, IRestHeadersCollection responseHeaders)
        {
            //This here is why I don't like JSON serialization. 😢
            //Note: on some services the headers should be checked for encoding 
            var markup = Encoding.UTF8.GetString(data);

            object markupAsObject = markup;

            return typeof(TResponseBody) == typeof(string) ? (TResponseBody)markupAsObject : JsonConvert.DeserializeObject<TResponseBody>(markup);
        }

        public byte[] Serialize<TRequestBody>(TRequestBody value, IRestHeadersCollection requestHeaders)
        {
            var json = JsonConvert.SerializeObject(value);

            //This here is why I don't like JSON serialization. 😢
            var binary = Encoding.UTF8.GetBytes(json);

            return binary;
        }
        #endregion
    }
}
