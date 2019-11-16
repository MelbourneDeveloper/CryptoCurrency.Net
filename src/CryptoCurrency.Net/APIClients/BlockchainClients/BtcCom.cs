using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.APIClients.BlockchainClients.CallArguments;
using CryptoCurrency.Net.Model;
using RestClientDotNet;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    public class BtcCom : BlockchainClientBase, IBlockchainClient
    {
        #region Constructor
        public BtcCom(CurrencySymbol currency, IRestClientFactory restClientFactory) : base(currency, restClientFactory)
        {
        }
        #endregion

        #region Private Static Fields
        public static CurrencyCapabilityCollection CurrencyCapabilities { get; } = new CurrencyCapabilityCollection { CurrencySymbol.BitcoinCash };
        #endregion

        #region Overrides
        protected override Func<GetAddressesArgs, Task<IEnumerable<BlockChainAddressInformation>>> GetAddressesFunc { get; } = async getAddressesArgs =>
        {
            return null;
        };

        public override Task<BlockChainAddressInformation> GetAddress(string address)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
