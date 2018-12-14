using Newtonsoft.Json;

namespace CryptoCurrency.Net.Model.JSONRPC
{
    public class TransactionCall
    {
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }

        [JsonProperty(PropertyName = "to")]
        public string To { get; set; }

        [JsonProperty(PropertyName = "gas", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long Gas { get; set; }

        [JsonProperty(PropertyName = "gasPrice", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long GasPrice { get; set; }

        [JsonProperty(PropertyName = "value", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long Value { get; set; }
    }
}
