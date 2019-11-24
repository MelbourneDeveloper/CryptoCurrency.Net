using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CryptoCurrency.Net.Base.Model;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients
{
    public abstract class ExchangeAPIClientBase : APIClientBase
    {
        #region Enums
        public enum PriceType
        {
            Bid,
            Ask
        }
        #endregion

        #region Protected Properties
        protected string ApiKey { get; }
        protected string ApiSecret { get; }
        #endregion

        #region Public Abstract Methods
        public abstract Task<GetHoldingsResult> GetHoldings(object tag);
        public abstract Task<Collection<ExchangePairPrice>> GetPairs(CurrencySymbol baseSymbol, PriceType priceType);
        #endregion

        #region Constructor
        protected ExchangeAPIClientBase(string apiKey, string apiSecret, IRestClientFactory restClientFactory) : base(restClientFactory)
        {
            ApiKey = apiKey;
            ApiSecret = apiSecret;
        }
        #endregion
    }
}
