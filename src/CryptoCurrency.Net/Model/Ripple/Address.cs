using System.Collections.Generic;

namespace CryptoCurrency.Net.Model.Ripple
{
    public class Balance
    {
        public decimal value { get; set; }
    }

    public class Address
    {
        public List<Balance> balances { get; } = new List<Balance>();
    }
}
