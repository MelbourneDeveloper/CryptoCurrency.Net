using System.Collections.Generic;

namespace CryptoCurrency.Net.APIClients.Model.Binance
{
    public class Balance
    {
        public string asset { get; set; }
        public decimal free { get; set; }
        public decimal locked { get; set; }

        public decimal CompleteValue => free + locked;
    }

    public class Account
    {
        public List<Balance> balances { get; } = new List<Balance>();
    }
}
