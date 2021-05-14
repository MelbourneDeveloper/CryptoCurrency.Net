﻿using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.APIClients.Model.Ethplorer;
using CryptoCurrency.Net.Base.Model;
using Microsoft.Extensions.Logging;
using RestClient.Net;
using RestClient.Net.Abstractions;
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
        public EthplorerClient(
            CurrencySymbol currency,
            Func<Uri, IClient> restClientFactory,
            ILogger<EthplorerClient> logger) : base(currency, restClientFactory, logger)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            var baseUri = new Uri("https://api.ethplorer.io");
            RESTClient = RESTClientFactory(baseUri);
            RESTClient.BaseUri = baseUri;
        }
        #endregion

        #region Func
        /// <inheritdoc />
        /// <summary>
        /// getAddressInfo method requests per minute: 10
        /// </summary>
        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));

            address = address.ToLower();

            //https://github.com/EverexIO/Ethplorer/wiki/Ethplorer-API#personal-key-limits
            //600 calls per minute = ten calls per second, but multiply that by 3 to be safe
            await Task.Delay(300);

            var addressModel = await GetAddressModel(address);
            if (addressModel == null)
            {
                throw new GetAddressException("ethplorer no good");
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
            var apiKeyPart = $"?apiKey={(!string.IsNullOrEmpty(APIKey) ? APIKey : "freekey")}";
            return await RESTClient.GetAsync<Address>($"/getAddressInfo/{address}{apiKeyPart}");
        }

        public async Task<IEnumerable<TokenBalance>> GetTokenBalances(IEnumerable<string> addresses)
        {
            if (addresses == null) throw new ArgumentNullException(nameof(addresses));

            var retVal = new List<TokenBalance>();

            foreach (var casedAddress in addresses)
            {
                var address = casedAddress.ToLower();

                var addressModel = _CachedAddresses.ContainsKey(address) ? _CachedAddresses[address] : await GetAddressModel(address);
                if (addressModel.tokens == null) continue;
                foreach (var token in addressModel.tokens)
                {
                    decimal balance = token.balance;

                    for (var i = 0; i < token.tokenInfo.decimals; i++)
                    {
                        balance /= 10;
                    }

                    retVal.Add(new TokenBalance(new CurrencySymbol(token.tokenInfo.symbol), balance, address, null));
                }
            }

            return retVal;
        }
        #endregion
    }
}
