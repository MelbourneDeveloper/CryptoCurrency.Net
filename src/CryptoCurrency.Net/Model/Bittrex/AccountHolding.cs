using System.Collections.Generic;

namespace CryptoCurrency.Net.Model.Bittrex
{
    public class AccountHolding
    {
        public string Currency { get; set; }
        public decimal Balance { get; set; }
    }

    public class GetBalancesResult
    {
        public bool success { get; set; }
        public List<AccountHolding> result { get; set; }
    }

}
