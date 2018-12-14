using Newtonsoft.Json;

namespace CryptoCurrency.Net.Model.Bitfinex
{
    public class BitfinexPostBase
    {
        [JsonProperty("request")]
        public string Request { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }
    }
}
