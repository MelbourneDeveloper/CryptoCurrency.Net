using System.Collections.Generic;

namespace CryptoCurrency.Net.Model.Blockscout
{
    public class BalanceMultiResult
    {
        public string balance { get; set; }
        public string account { get; set; }
    }

    public class BalanceMulti
    {
        public string status { get; set; }
        public List<BalanceMultiResult> result { get; set; }
        public string message { get; set; }
    }
}
