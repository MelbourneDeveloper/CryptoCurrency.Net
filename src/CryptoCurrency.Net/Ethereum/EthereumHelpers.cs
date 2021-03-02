namespace CryptoCurrency.Net.Ethereum
{
    public static class EthereumHelpers
    {
        public static decimal ToEthereumBalance(this string wei)
        {

            wei = wei.PadLeft(32,'0');
            wei = wei.Insert(14, ".");


            var asdasd =  decimal.Parse(wei);

            return asdasd;
        }
    }
}
