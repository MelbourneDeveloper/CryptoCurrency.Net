using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.PriceEstimatation;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients.PriceEstimationClients
{
    public class PriceEstimationManager
    {
        #region Fields
        private readonly Collection<IPriceEstimationClient> _Clients = new Collection<IPriceEstimationClient>();
        #endregion

        #region Constructor
        public PriceEstimationManager(IRestClientFactory restClientFactory)
        {
            foreach (var typeInfo in typeof(PriceEstimationManager).GetTypeInfo().Assembly.DefinedTypes)
            {
                var type = typeInfo.AsType();

                if (typeInfo.ImplementedInterfaces.Contains(typeof(IPriceEstimationClient)))
                {
                    _Clients.Add((IPriceEstimationClient)Activator.CreateInstance(type, restClientFactory));
                }
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// TODO: This needs to be averaged. The two current clients give wildly different values. Need to include some Australian exchanges etc.
        /// </summary>
        public async Task<EstimatedPricesModel> GetPrices(IEnumerable<CurrencySymbol> currencySymbols, string fiatCurrency)
        {
            //Lets try a client that hasn't been used before if there is one
            var client = _Clients.FirstOrDefault(c => c.AverageCallTimespan.TotalMilliseconds == 0);
            var currencies = currencySymbols.ToList();
            if (client != null)
            {
                try
                {
                    return await client.GetPrices(currencies, fiatCurrency);
                }
                catch
                {
                    //Do nothing
                }
            }

            foreach (var client2 in _Clients.OrderBy(c => c.SuccessRate).ThenBy(c => c.AverageCallTimespan).ToList())
            {
                try
                {
                    return await client2.GetPrices(currencies, fiatCurrency);
                }
                catch (Exception ex)
                {
                    Logger.Log("Error Getting Prices", ex, nameof(PriceEstimationManager));
                }
            }

            throw new GetPricesException("Can't get prices");
        }
        #endregion
    }
}
