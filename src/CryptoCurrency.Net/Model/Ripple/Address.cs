using System.Collections.Generic;

namespace CryptoCurrency.Net.Model.Ripple
{
    public class Balance
    {
        public decimal value { get; set; }
    }

    public class Address
    {
        public string message { get; set; }
        public string result { get; set; }
        public List<Balance> balances { get; } = new List<Balance>();
    }
}
