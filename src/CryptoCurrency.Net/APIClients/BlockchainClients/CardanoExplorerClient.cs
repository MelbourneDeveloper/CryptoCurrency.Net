using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.CardanoExplorer;
using RestClientDotNet;
using System;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    /// <summary>
    /// https://cardanodocs.com/technical/explorer/api/
    /// </summary>
    public class CardanoExplorer : BlockchainClientBase, IBlockchainClient
    {
        #region Public Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.Cardano };
        #endregion

        #region Constructor
        public CardanoExplorer(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            RESTClient = restClientFactory.CreateRESTClient(new Uri("https://cardanoexplorer.com"));
        }

        public async override Task<BlockChainAddressInformation> GetAddress(string address)
        {
            var addressResult = await  RESTClient.GetAsync<Address>($"api/addresses/summary/{address}");
            return new BlockChainAddressInformation(address, addressResult.Right.caBalance.getCoin, addressResult.Right.caTxList.Count);
        }
        #endregion


    }
}
