namespace CryptoCurrency.Net.Base.Model
{
    public class ExchangePairPrice
    {
        public CurrencySymbol BaseSymbol { get; set; }
        public CurrencySymbol ToSymbol { get; set; }
        public decimal Price { get; set; }
        public decimal? Volume { get; }

        public ExchangePairPrice(decimal? volume)
        {
            Volume = volume;
        }
    }
}
