using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CryptoCurrency.Net.Model.JSONRPC
{
    public class RequestBase
    {
        /// <summary>
        /// A String specifying the version of the JSON-RPC protocol.MUST be exactly "2.0".
        /// </summary>   
        [JsonProperty(PropertyName = "jsonrpc")]
        public string JSONRPC { get; } = "2.0";

        /// <summary>
        /// A String containing the name of the method to be invoked. Method names that begin with the word rpc followed by a period character (U+002E or ASCII 46) are reserved for rpc-internal methods and extensions and MUST NOT be used for anything else.
        /// </summary>
        [JsonProperty(PropertyName = "method")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Method Method { get; set; }

        /// <summary>
        /// A Structured value that holds the parameter values to be used during the invocation of the method.This member MAY be omitted.
        /// </summary>
        [JsonProperty(PropertyName = "params")]
        public List<object> Params { get; } = new List<object>();

        /// <summary>
        /// An identifier established by the Client that MUST contain a String, Number, or NULL value if included.If it is not included it is assumed to be a notification.The value SHOULD normally not be Null[1] and Numbers SHOULD NOT contain fractional parts[2]
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
    }
}
