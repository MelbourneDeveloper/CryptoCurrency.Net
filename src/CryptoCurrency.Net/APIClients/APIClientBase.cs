using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestClientDotNet;

namespace CryptoCurrency.Net.APIClients
{
    public abstract class APIClientBase
    {
        #region Private Fields
        private readonly List<TimeSpan> _CallTimes = new List<TimeSpan>();
        private IRestClientFactory RESTClientFactory { get; }
        #endregion

        #region Protected Properties
        protected RestClient RESTClient { get; set; }
        #endregion

        #region Public Properties
        public Uri BaseUri => RESTClient?.BaseUri;
        public int SuccessfulCallCount { get; private set; }
        public int CallCount { get; private set; }

        public decimal SuccessRate
        {
            get
            {
                if (CallCount == 0)
                {
                    return 1;
                }

                if (SuccessfulCallCount == 0)
                {
                    return 0;
                }

                return SuccessfulCallCount / (decimal)CallCount;
            }
        }

        public TimeSpan AverageCallTimespan => _CallTimes.Count == 0 ? new TimeSpan() : new TimeSpan(0, 0, 0, 0, (int)_CallTimes.Average(c => c.TotalMilliseconds));
        #endregion

        #region Constructor
        protected APIClientBase(IRestClientFactory restClientFactory)
        {
            RESTClientFactory = restClientFactory;
        }
        #endregion

        #region Protected Methods
        protected async Task<T> Call<T>(Delegate func, object arg)
        {
            var startTime = DateTime.Now;
            CallCount++;
            var task = (Task<T>)func.DynamicInvoke(arg);
            var retVal = await task;
            _CallTimes.Add(DateTime.Now - startTime);
            SuccessfulCallCount++;
            return retVal;
        }
        #endregion
    }
}
