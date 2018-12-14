namespace CryptoCurrency.Net.Model
{
    public class TokenBalance
    {
        public CurrencySymbol CurrencySymbol { get; }
        public decimal Balance { get; }
        public string EthereumAddress { get; }
        public string Contract { get; }

        public TokenBalance(CurrencySymbol currencySymbol, decimal balance, string ethereumAddress, string contract)
        {
            CurrencySymbol = currencySymbol;
            Balance = balance;
            EthereumAddress = ethereumAddress;
            Contract = contract;
        }
    }
}
