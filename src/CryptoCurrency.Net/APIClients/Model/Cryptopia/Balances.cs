using System.Collections.Generic;

namespace CryptoCurrency.Net.APIClients.Model.Cryptopia
{
    public class Balance
    {
        public string Symbol { get; set; }
        public decimal Total { get; set; }
    }

    public class Balances
    {
        public List<Balance> Data { get; } = new List<Balance>();
    }
}
