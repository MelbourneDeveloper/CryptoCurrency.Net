using System.Collections.Generic;

namespace CryptoCurrency.Net.Model.Octgo
{
    public class Balance
    {
        public string name { get; set; }
        public decimal total { get; set; }
    }

    public class Address
    {
        public List<Balance> balances { get; } = new List<Balance>();
    }
}
