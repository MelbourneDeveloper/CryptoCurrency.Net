using System;
using System.Threading.Tasks;
using CryptoCurrency.Net.Model;

namespace CryptoCurrency.Net
{
    public interface IExchangeAPIClient
    {
        /// <summary>
        /// Gets the holdings of non-zero currencies for the given account
        /// </summary>
        Task<GetHoldingsResult> GetHoldings(object tag);
        Uri BaseUri { get; }
    }
}
