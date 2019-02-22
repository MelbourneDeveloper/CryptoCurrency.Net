using System.Collections.Generic;

namespace CryptoCurrency.Net.Model.Insight
{
    public class ScriptSig
    {
        public string hex { get; set; }
        public string asm { get; set; }
    }

    public class Vin
    {
        public string txid { get; set; }
        public int vout { get; set; }
        public long sequence { get; set; }
        public int n { get; set; }
        public ScriptSig scriptSig { get; set; }
        public string addr { get; set; }
        public long valueSat { get; set; }
        public decimal value { get; set; }
        public object decimalSpentTxID { get; set; }
    }

    public class ScriptPubKey
    {
        public string hex { get; set; }
        public string asm { get; set; }
        public List<string> addresses { get; } = new List<string>();
        public string type { get; set; }
    }

    public class Vout
    {
        public decimal value { get; set; }
        public int n { get; set; }
        public ScriptPubKey scriptPubKey { get; set; }
        public object spentTxId { get; set; }
        public object spentIndex { get; set; }
        public object spentHeight { get; set; }
    }

    public class Transaction
    {
        public string txid { get; set; }
        public int version { get; set; }
        public int locktime { get; set; }
        public List<Vin> vin { get; } = new List<Vin>();
        public List<Vout> vout { get; } = new List<Vout>();
        public string blockhash { get; set; }
        public int blockheight { get; set; }
        public int confirmations { get; set; }
        public int time { get; set; }
        public int blocktime { get; set; }
        public decimal valueOut { get; set; }
        public int size { get; set; }
        public decimal valueIn { get; set; }
        public decimal fees { get; set; }
    }
}
