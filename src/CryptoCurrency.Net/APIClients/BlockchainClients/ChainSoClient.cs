using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.APIClients.Model.ChainSo;
using CryptoCurrency.Net.Base.Model;
using Microsoft.Extensions.Logging;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    public class ChainSoClient : BlockchainClientBase, IBlockchainClient, IDisposable
    {
        #region Private Fields
        private static readonly SemaphoreSlim _SemaphoreSlim = new(1, 1);
        private bool disposed;
        #endregion

        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new() { CurrencySymbol.DogeCoin };
        #endregion

        #region Constructor
        public ChainSoClient(CurrencySymbol currency, Func<Uri, IClient> restClientFactory, ILogger<ChainSoClient> logger) : base(currency, restClientFactory, logger)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            var baseUri = new Uri("https://chain.so");
            RESTClient = RESTClientFactory(baseUri);
            RESTClient.BaseUri = baseUri;
        }
        #endregion

        #region Func
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            try
            {
                await _SemaphoreSlim.WaitAsync();

                //https://chain.so/api#rate-limits
                //5 requests/second * 3
                await Task.Delay(400);

                ChainSoAddress balance = await RESTClient.GetAsync<ChainSoAddress>($"/api/v2/get_address_balance/{Currency}/{address}");
                if (balance.data.confirmed_balance != 0)
                {
                    //TODO: This should include both confirmed and unconformed...
                    return new BlockChainAddressInformation(address, balance.data.confirmed_balance, false);
                }

                //There is no balance so check to see if the address was ever used
                await Task.Delay(400);

                ChainSoAddressReceived received = await RESTClient.GetAsync<ChainSoAddressReceived>($"/api/v2/get_address_received/{Currency}/{address}");

                return new BlockChainAddressInformation(address, balance.data.confirmed_balance, received.data.confirmed_received_value == 0);
            }
            finally
            {
                _SemaphoreSlim.Release();
            }
        }

        public void Dispose()
        {
            if (disposed) return;
            disposed = true;
            _SemaphoreSlim.Dispose();
        }
        #endregion
    }
}
