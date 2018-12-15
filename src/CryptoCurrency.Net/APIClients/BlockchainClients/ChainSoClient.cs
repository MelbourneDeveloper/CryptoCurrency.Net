using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.ChainSo;
using RestClientDotNet;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    public class ChainSoClient : BlockchainClientBase, IBlockchainClient
    {
        #region Private Fields
        private SemaphoreSlim _SemaphoreSlim = new SemaphoreSlim(1);
        #endregion

        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities => new CurrencyCapabilityCollection { CurrencySymbol.DogeCoin};
        #endregion

        #region Constructor
        public ChainSoClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            RESTClient = restClientFactory.CreateRESTClient(new Uri("https://chain.so"));
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

                var balance = await RESTClient.GetAsync<ChainSoAddress>($"/api/v2/get_address_balance/{Currency}/{address}");
                if (balance.data.confirmed_balance != 0)
                {
                    //TODO: This should include both confirmed and unconformed...
                    return new BlockChainAddressInformation(address, balance.data.confirmed_balance, false);
                }

                //There is no balance so check to see if the address was ever used
                await Task.Delay(400);

                var received = await RESTClient.GetAsync<ChainSoAddressReceived>($"/api/v2/get_address_received/{Currency}/{address}");

                return new BlockChainAddressInformation(address, balance.data.confirmed_balance, received.data.confirmed_received_value == 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _SemaphoreSlim.Release();
            }
        }
        #endregion
    }
}
