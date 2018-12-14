using System.Collections.Generic;

namespace CryptoCurrency.Net.Model.Cryptopia
{
    public class Balance
    {
        public string Symbol { get; set; }
        public decimal Total { get; set; }
    }

    public class Balances
    {
        public List<Balance> Data { get; set; }
    }
}
