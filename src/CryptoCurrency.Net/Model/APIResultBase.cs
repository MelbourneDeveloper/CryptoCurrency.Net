namespace CryptoCurrency.Net.Model
{
    public abstract class APIResultBase
    {
        public APIResultBase(object tag)
        {
            Tag = tag;
        }

        /// <summary>
        /// To be used with WhenAll calls
        /// </summary>
        public object Tag { get; }
    }
}
