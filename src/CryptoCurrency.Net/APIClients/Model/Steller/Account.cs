using System.Collections.Generic;

namespace CryptoCurrency.Net.APIClients.Model.Steller
{
    public class Balance
    {
        public decimal balance { get; set; }
    }

    public class Account
    {
        public string account_id { get; set; }
        public List<Balance> balances { get; } = new List<Balance>();
    }
}
