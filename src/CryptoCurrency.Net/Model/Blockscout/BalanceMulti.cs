using System.Collections.Generic;

namespace CryptoCurrency.Net.Model.Blockscout
{
    public class Result
    {
        public string balance { get; set; }
        public string account { get; set; }
    }

    public class BalanceMulti
    {
        public string status { get; set; }
        public List<Result> result { get; set; }
        public string message { get; set; }
    }
}
