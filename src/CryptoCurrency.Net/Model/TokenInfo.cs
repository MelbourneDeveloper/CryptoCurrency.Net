namespace CryptoCurrency.Net.Model
{
    public class TokenInfo
    {
        public string Symbol { get; }
        public string Contract { get; }
        public int Decimals { get; }

        public TokenInfo(string symbol, string contract, int decimals)
        {
            Symbol = symbol;
            Contract = contract;
            Decimals = decimals;
        }

    }
}
