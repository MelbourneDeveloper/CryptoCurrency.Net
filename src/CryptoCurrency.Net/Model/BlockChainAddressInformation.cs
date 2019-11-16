using System;

namespace CryptoCurrency.Net.Model
{
    /// <summary>
    /// TODO: Does this really need to implement INotifyPropertyChanged? It's not really view anywhere yet...
    /// </summary>
    [Serializable]
    public class BlockChainAddressInformation : ModelBase
    {
        #region Private Fields
        private DateTime? _LastUpdate;
        #endregion

        #region Public Properties
        public string Address { get; set; }
        public int? TransactionCount { get; set; }
        public decimal? Balance { get; set; }
        public bool? IsChange { get; set; }
        public uint? Index { get; set; }
        public uint? Account { get; set; }
        public DateTime? LastUpdate
        {
            get => _LastUpdate;
            set
            {
                _LastUpdate = value;
                RaisePropertyChanged(nameof(LastUpdate));
            }
        }

        public bool? IsUnused { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Only for Serialization!
        /// </summary>
        public BlockChainAddressInformation()
        {
        }

        private BlockChainAddressInformation(string address, decimal? balance)
        {
            Address = address;
            Balance = balance;
        }

        public BlockChainAddressInformation(string address, decimal? balance, int transactionCount) : this(address, balance)
        {
            TransactionCount = transactionCount;
        }

        public BlockChainAddressInformation(string address, decimal? balance, bool isUnused) : this(address, balance)
        {
            IsUnused = isUnused;
        }

        //[Obsolete("This overload should never be used because it causes a given coin to get jammed up")]
        public BlockChainAddressInformation(string address, int? transactionCount, decimal? balance) : this(address, balance)
        {
            if (transactionCount.HasValue)
            {
                TransactionCount = transactionCount;
            }
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
                    return Address.Equals(blockChainAddressInformation.Address, StringComparison.OrdinalIgnoreCase);
                case string objAsString:
                    return Address.Equals(objAsString, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
        #endregion
    }
}
