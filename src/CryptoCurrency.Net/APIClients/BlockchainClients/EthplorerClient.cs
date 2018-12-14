using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.Ethplorer;
using RestClientDotNet;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    /// <summary>
    /// https://github.com/EverexIO/Ethplorer/wiki/Ethplorer-API
    /// </summary>
    public class EthplorerClient : BlockchainClientBase, IBlockchainClient, ITokenClient
    {
        #region Public Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.Ethereum };
        #endregion

        #region Public Static Properties
        public static string APIKey { get; set; }
        #endregion

        #region Private Static Fields
        private readonly Dictionary<string, Address> _CachedAddresses = new Dictionary<string, Address>();
        #endregion

        #region Constructor
        public EthplorerClient(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            RESTClient = restClientFactory.CreateRESTClient(new Uri("https://api.ethplorer.io"));
        }
        #endregion

        #region Func
        /// <inheritdoc />
        /// <summary>
        /// getAddressInfo method requests per minute: 10
        /// </summary>
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            address = address.ToLower();

            //https://github.com/EverexIO/Ethplorer/wiki/Ethplorer-API#personal-key-limits
            //600 calls per minute = ten calls per second, but multiply that by 3 to be safe
            await Task.Delay(300);

            var addressModel = await GetAddressModel(address);
            if (addressModel == null)
            {
                throw new Exception("ethplorer no good");
            }

            if (_CachedAddresses.ContainsKey(address))
            {
                _CachedAddresses.Remove(address);
            }

            _CachedAddresses.Add(address, addressModel);

            return new BlockChainAddressInformation(address, addressModel.ETH.balance, addressModel.countTxs);
        }

        private async Task<Address> GetAddressModel(string address)
        {
            var apiKeyPart = !string.IsNullOrEmpty(APIKey) ? $"?apiKey={APIKey}" : string.Empty;
            return await RESTClient.GetAsync<Address>($"/getAddressInfo/{address}{apiKeyPart}");
        }

        public async Task<IEnumerable<TokenBalance>> GetTokenBalances(IEnumerable<string> addresses)
        {
            var retVal = new List<TokenBalance>();

            foreach (var casedAddress in addresses)
            {
                var address = casedAddress.ToLower();

                Address addressModel;

                if (_CachedAddresses.ContainsKey(address))
                {
                    addressModel = _CachedAddresses[address];
                }
                else
                {
                    addressModel = await GetAddressModel(address);
                }

                if (addressModel.tokens == null) continue;
                foreach (var token in addressModel.tokens)
                {
                    decimal balance = token.balance;

                    for (var i = 0; i < token.tokenInfo.decimals; i++)
                    {
                        balance = balance / 10;
                    }

                    retVal.Add(new TokenBalance(new CurrencySymbol(token.tokenInfo.symbol), balance, address, null));
                }
            }

            return retVal;
        }
        #endregion
    }
}
