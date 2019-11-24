using Newtonsoft.Json;

namespace CryptoCurrency.Net.APIClients.Model.Bitfinex
{
    public class BitfinexPostBase
    {
        [JsonProperty("request")]
        public string Request { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }
    }
}
