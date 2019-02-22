using System.Collections.Generic;

namespace CryptoCurrency.Net.Model.Bittrex
{
    public class Values
    {
        public string MarketName { get; set; }
        public decimal Volume { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
    }

    public class Markets
    {
        public List<Values> result { get; } = new List<Values>();
    }
}
