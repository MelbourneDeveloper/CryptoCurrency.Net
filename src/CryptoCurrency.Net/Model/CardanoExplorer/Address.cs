using System.Collections.Generic;

#pragma warning disable CA2227 

namespace CryptoCurrency.Net.Model.CardanoExplorer
{
    public class CaBalance
    {
        public long getCoin { get; set; }
    }

    public class CtbInputSum
    {
        public long getCoin { get; set; }
    }

    public class CtbOutputSum
    {
        public long getCoin { get; set; }
    }

    public class CaTxList
    {
        public string ctbId { get; set; }
        public int ctbTimeIssued { get; set; }
        public List<List<object>> ctbInputs { get; set; }
        public List<List<object>> ctbOutputs { get; set; }
        public CtbInputSum ctbInputSum { get; set; }
        public CtbOutputSum ctbOutputSum { get; set; }
    }

    public class Right
    {
        public string caAddress { get; set; }
        public string caType { get; set; }
        public int caTxNum { get; set; }
        public CaBalance caBalance { get; set; }
        public List<CaTxList> caTxList { get; set; }
    }

    public class Address
    {
        public Right Right { get; set; }
    }
}
#pragma warning restore CA2227