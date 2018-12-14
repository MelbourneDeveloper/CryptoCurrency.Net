using System.Collections.Generic;

namespace CryptoCurrency.Net.Model.Insight
{
    public class Address
    {
        public decimal balance { get; set; }
        public List<string> transactions { get; set; }
    }
}
