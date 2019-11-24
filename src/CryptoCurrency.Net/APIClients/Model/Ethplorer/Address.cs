using System.Collections.Generic;

namespace CryptoCurrency.Net.APIClients.Model.Ethplorer
{
    public class ETH
    {
        public decimal balance { get; set; }
    }

    public class TokenInfo
    {
        public int decimals { get; set; }
        public string symbol { get; set; }
    }

    public class Token
    {
        public TokenInfo tokenInfo { get; set; }
        public long balance { get; set; }
        public long totalIn { get; set; }
        public long totalOut { get; set; }
    }

    public class Address
    {
        public ETH ETH { get; set; }
        public int countTxs { get; set; }
        public List<Token> tokens { get; } = new List<Token>();
    }
}
