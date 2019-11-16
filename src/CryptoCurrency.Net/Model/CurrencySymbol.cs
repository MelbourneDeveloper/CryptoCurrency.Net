using System;
using System.Collections.Generic;

namespace CryptoCurrency.Net.Model
{
    [Serializable]
    public class CurrencySymbol
    {
        #region Fields
        private string _Name;
        #endregion

        #region Public Properties
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value.ToUpper().Trim();

                //Use BTC instead of XBT
                if (_Name == "XBT")
                {
                    _Name = BitcoinSymbolName;
                }

                if (_Name == "IOTA")
                {
                    _Name = "IOT";
                }

                if (_Name == "MIOTA")
                {
                    _Name = "IOT";
                }

                if (_Name == "BCC")
                {
                    _Name = BitcoinCashSymbolName;
                }

                if (_Name == "XDG")
                {
                    _Name = "DOGE";
                }
            }
        }

        #endregion

        #region Public Constants
        public const decimal Satoshi = 100000000;
        public const decimal Wei = 1000000000000000000;
        public const string BitcoinCashSymbolName = "BCH";
        public const string BitcoinSymbolName = "BTC";
        public const string LitecoinSymbolName = "LTC";
        public const string EthereumClassicSymbolName = "ETC";
        public const string EthereumSymbolName = "ETH";
        public const string RippleSymbolName = "XRP";
        public const string NEOSymbolName = "NEO";
        public const string StellerSymbolName = "XLM";
        public const string DashSymbolName = "DASH";
        public const string BitcoinGoldSymbolName = "BTG";
        public const string ZCashSymbolName = "ZEC";
        public const string CrownSymbolName = "CRW";
        public const string CardanoSymbolName = "ADA";
        public const string DigiByteSymbolName = "DGB";
        public const string DogeCoinSymbolName = "DOGE";
        public const string VertCoinSymbolName = "VTC";
        public const string TronSymbolName = "TRX";

        public static CurrencySymbol Bitcoin { get; } = new CurrencySymbol(BitcoinSymbolName);
        public static CurrencySymbol Litecoin { get; } = new CurrencySymbol(LitecoinSymbolName);
        public static CurrencySymbol BitcoinCash { get; } = new CurrencySymbol(BitcoinCashSymbolName);
        public static CurrencySymbol Dash { get; } = new CurrencySymbol(DashSymbolName);
        public static CurrencySymbol BitcoinGold { get; } = new CurrencySymbol(BitcoinGoldSymbolName);
        public static CurrencySymbol ZCash { get; } = new CurrencySymbol(ZCashSymbolName);
        public static CurrencySymbol Ethereum { get; } = new CurrencySymbol(EthereumSymbolName);
        public static CurrencySymbol EthereumClassic { get; } = new CurrencySymbol(EthereumClassicSymbolName);
        public static CurrencySymbol Crown { get; } = new CurrencySymbol(CrownSymbolName);
        public static CurrencySymbol Cardano { get; } = new CurrencySymbol(CardanoSymbolName);
        public static CurrencySymbol DigiByte { get; } = new CurrencySymbol(DigiByteSymbolName);
        public static CurrencySymbol DogeCoin { get; } = new CurrencySymbol(DogeCoinSymbolName);
        public static CurrencySymbol VertCoin { get; } = new CurrencySymbol(VertCoinSymbolName);
        public static CurrencySymbol Ripple { get; } = new CurrencySymbol(RippleSymbolName);
        public static CurrencySymbol Neo { get; } = new CurrencySymbol(NEOSymbolName);
        public static CurrencySymbol Stellar { get; } = new CurrencySymbol(StellerSymbolName);
        public static CurrencySymbol Tron { get; } = new CurrencySymbol(TronSymbolName);
        #endregion

        #region Constructor
        /// <summary>
        /// Don't use this. Only here for serialization
        /// </summary>
        public CurrencySymbol()
        {

        }

        public CurrencySymbol(string name)
        {
            Name = name;
        }
        #endregion

        #region Public Methods
        public static bool IsEthereum(CurrencySymbol currencySymbol)
        {
            return new List<CurrencySymbol> { Ethereum, EthereumClassic }.Contains(currencySymbol);
        }

        public override bool Equals(object obj)
        {
            return !(obj is CurrencySymbol currencySymbol) ? false : string.Equals(currencySymbol.Name, Name, StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            var retVal = 0;
            foreach (var character in Name)
            {
                retVal += character;
            }

            return retVal;
        }

        public override string ToString()
        {
            return _Name;
        }

        #endregion
    }
}
