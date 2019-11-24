namespace CryptoCurrency.Net.Base.Model
{
    public abstract class APIResultBase
    {
        protected APIResultBase(object tag)
        {
            Tag = tag;
        }

        /// <summary>
        /// To be used with WhenAll calls
        /// </summary>
        public object Tag { get; }
    }
}
