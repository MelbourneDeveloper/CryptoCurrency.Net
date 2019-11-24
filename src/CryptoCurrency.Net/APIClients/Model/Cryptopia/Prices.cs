using System.Collections.Generic;

namespace CryptoCurrency.Net.APIClients.Model.Cryptopia
{
    public class Datum
    {
        public string Label { get; set; }
        public decimal AskPrice { get; set; }
        public decimal BidPrice { get; set; }
        public decimal Volume { get; set; }
        public decimal LastPrice { get; set; }
        public decimal Change { get; set; }
    }

    public class Prices
    {
        public List<Datum> Data { get; } = new List<Datum>();
    }
}
