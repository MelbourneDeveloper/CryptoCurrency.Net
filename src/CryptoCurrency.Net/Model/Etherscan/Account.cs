using System.Collections.Generic;

namespace CryptoCurrency.Net.Model.Etherscan
{
    public class Result
    {
        public string account { get; set; }
        public long balance { get; set; }
    }

    public class Account
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Result> result { get; set; }
    }
}
