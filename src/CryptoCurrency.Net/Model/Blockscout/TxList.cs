using System.Collections.Generic;

namespace CryptoCurrency.Net.Model.Blockscout
{
    public class TxListResult
    {
        public string value { get; set; }
        public string txreceipt_status { get; set; }
        public string transactionIndex { get; set; }
        public string to { get; set; }
        public string timeStamp { get; set; }
        public string nonce { get; set; }
        public string isError { get; set; }
        public string input { get; set; }
        public string hash { get; set; }
        public string gasUsed { get; set; }
        public string gasPrice { get; set; }
        public string gas { get; set; }
        public string from { get; set; }
        public string cumulativeGasUsed { get; set; }
        public string contractAddress { get; set; }
        public string confirmations { get; set; }
        public string blockNumber { get; set; }
        public string blockHash { get; set; }
    }

    public class TxList
    {
        public string status { get; set; }
        public List<TxListResult> result { get; set; }
        public string message { get; set; }

        /// <summary>
        /// This is not part of the Json payload. It's used to keep track of what address was queried
        /// </summary>
        public string Address { get; set; }
    }
}
