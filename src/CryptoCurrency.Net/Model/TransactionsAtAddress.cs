using System;
using System.Collections.Generic;

namespace CryptoCurrency.Net.Model
{
    [Serializable]
    public class TransactionsAtAddress : ModelBase
    {
        #region Private Fields
        private DateTime? _LastUpdate;
        #endregion

        #region Public Properties
        public string Address { get; set; }
        public List<Transaction> Transactions { get; } = new List<Transaction>();
        public DateTime? LastUpdate
        {
            get => _LastUpdate;
            set
            {
                _LastUpdate = value;
                RaisePropertyChanged(nameof(LastUpdate));
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Only for Serialization!
        /// </summary>
        public TransactionsAtAddress()
        {
        }

        public TransactionsAtAddress(string address, IEnumerable<Transaction> transactions)
        {
            Address = address;
            Transactions.AddRange(transactions);
        }
        #endregion

        #region Public Methods
        public override bool Equals(object obj)
        {
            if (Address == null)
            {
                return false;
            }

            switch (obj)
            {
                case BlockChainAddressInformation blockChainAddressInformation:
                    return Address.Equals(blockChainAddressInformation.Address, StringComparison.CurrentCultureIgnoreCase);
                case string objAsString:
                    return Address.Equals(objAsString, StringComparison.CurrentCultureIgnoreCase);
            }

            return false;
        }
        #endregion
    }
}
