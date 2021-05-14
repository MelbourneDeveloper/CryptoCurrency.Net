using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace CryptoCurrency.Net.Base.Model
{
    [Serializable]
    public class CurrencyHolding
    {
        #region Public Properties
        public CurrencySymbol Symbol { get; set; }
        public ObservableCollection<BlockChainAddressInformation> BlockChainAddresses { get; } = new ObservableCollection<BlockChainAddressInformation>();
        public bool IsToken { get; set; }

        [XmlIgnore]
        public decimal HoldingAmount => BlockChainAddresses.Select(bca => bca.Balance ?? 0).Sum();
        public BlockChainAddressInformation StalestBlockchainAddress
        {
            get
            {
                BlockChainAddressInformation retVal = null;

                foreach (var blockChainAddress in BlockChainAddresses)
                {
                    if (blockChainAddress.LastUpdate == null)
                    {
                        return blockChainAddress;
                    }

                    if (retVal == null)
                    {
                        retVal = blockChainAddress;
                    }

                    if (blockChainAddress.LastUpdate < retVal.LastUpdate)
                    {
                        retVal = blockChainAddress;
                    }

                }

                return retVal;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Only for serialization
        /// </summary>
        public CurrencyHolding()
        {

        }

        public CurrencyHolding(CurrencySymbol symbol) => Symbol = symbol;

        public CurrencyHolding(CurrencySymbol symbol, BlockChainAddressInformation blockChainAddressInformation)
        {
            Symbol = symbol;
            BlockChainAddresses.Add(blockChainAddressInformation);
        }
        #endregion
    }
}
