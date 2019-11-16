using CryptoCurrency.Net.Model;
using RestClientDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients.BlockchainClients
{
    public class BlockchainClientManager
    {
        #region  Fields
        private Dictionary<CurrencySymbol, List<IBlockchainClient>> _BlockchainClientsByCurrencySymbol = new Dictionary<CurrencySymbol, List<IBlockchainClient>>();
        private readonly Dictionary<Type, CurrencyCapabilityCollection> _CapabilitiesByClientType = new Dictionary<Type, CurrencyCapabilityCollection>();
        private readonly IRestClientFactory _RESTClientFactory;
        #endregion

        #region Static Constructor
        public BlockchainClientManager(IRestClientFactory restClientFactory)
        {
            _RESTClientFactory = restClientFactory;

            foreach (var typeInfo in typeof(BlockchainClientManager).GetTypeInfo().Assembly.DefinedTypes)
            {
                var type = typeInfo.AsType();

                if (!typeInfo.ImplementedInterfaces.Contains(typeof(IBlockchainClient))) continue;

                var currencyCapabilityProperty = typeInfo.GetDeclaredProperty("CurrencyCapabilities");

                var capabilityCollection = (CurrencyCapabilityCollection)currencyCapabilityProperty.GetValue(null);

                foreach (var currencySymbol in capabilityCollection)
                {
                    if (!_BlockchainClientsByCurrencySymbol.TryGetValue(currencySymbol, out _))
                    {
                        _BlockchainClientsByCurrencySymbol.Add(currencySymbol, new List<IBlockchainClient>());
                    }

                    var blockChainClient = (IBlockchainClient)Activator.CreateInstance(type, currencySymbol, restClientFactory);

                    _BlockchainClientsByCurrencySymbol[currencySymbol].Add(blockChainClient);
                }

                lock (_CapabilitiesByClientType)
                {
                    if (!_CapabilitiesByClientType.ContainsKey(type))
                    {
                        _CapabilitiesByClientType.Add(type, capabilityCollection);
                    }
                }
            }
        }
        #endregion

        #region Public Methods
        private static void ShuffleNotTestedClientToFirst(IList<IBlockchainClient> clients)
        {
            //Stick any clients that have never been called first so they get a chance
            var notCalledClient = clients.FirstOrDefault(c => c.CallCount == 0);
            if (notCalledClient == null) return;
            clients.Remove(notCalledClient);
            clients.Insert(0, notCalledClient);
        }

        public async Task<Dictionary<CurrencySymbol, IEnumerable<BlockChainAddressInformation>>> GetAddresses(CurrencySymbol currencySymbol, IEnumerable<string> addresses)
        {
            if (currencySymbol == null) throw new ArgumentNullException(nameof(currencySymbol));

            var addressList = addresses.ToList();

            var retVal = new Dictionary<CurrencySymbol, IEnumerable<BlockChainAddressInformation>>();

            Exception lastException = null;

            if (!_BlockchainClientsByCurrencySymbol.TryGetValue(currencySymbol, out var blockChainClients))
                throw new NotImplementedException(
                    $"The currency {currencySymbol} is not currently supported, or the Blockchain services are out of action.",
                    lastException);

            var capableclientTypes = _CapabilitiesByClientType.Keys.Where(clientType => _CapabilitiesByClientType[clientType] != null);

            var capableClients = blockChainClients.Where(bcc => capableclientTypes.Contains(bcc.GetType()));

            //Don't need to worry about CanGetMultipleAddresses because this will be faster so it will automatically bubble to the top

            var clients = capableClients.OrderByDescending(bcc => bcc.SuccessRate).ThenBy(bcc => bcc.AverageCallTimespan.TotalMilliseconds).ToList();

            ShuffleNotTestedClientToFirst(clients);

            foreach (var client in clients)
            {
                try
                {
                    try
                    {
                        var blockchainAddressInformations = await client.GetAddresses(addressList);
                        retVal.Add(currencySymbol, blockchainAddressInformations);

                        if (!currencySymbol.Equals(CurrencySymbol.Ethereum)) return retVal;

                        var tokenBalances = await new InfuraJSONRPCClient(CurrencySymbol.Ethereum, _RESTClientFactory).GetTokenBalances(blockchainAddressInformations.Select(b => b.Address));

                        foreach (var tokenBalance in tokenBalances)
                        {
                            retVal.Add(tokenBalance.CurrencySymbol, new List<BlockChainAddressInformation> { new BlockChainAddressInformation(tokenBalance.EthereumAddress, tokenBalance.Balance, false) });
                        }

                        return retVal;
                    }
                    catch (Exception ex)
                    {
                        lastException = ex;
                        Logger.Log($"Get Addresses failed. Client: {client.GetType().FullName}. Symbol: {currencySymbol}", ex, BlockchainClientBase.LogSection);
                    }
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    Logger.Log($"Get Addresses failed. Client: {client.GetType().FullName}. Symbol: {currencySymbol}", ex, BlockchainClientBase.LogSection);
                }
            }

            //TODO: reimplement code for taking stabs on coins that we don't know about
            throw new NotImplementedException($"The currency {currencySymbol} is not currently supported, or the Blockchain services are out of action.", lastException);
        }
        #endregion
    }
}
