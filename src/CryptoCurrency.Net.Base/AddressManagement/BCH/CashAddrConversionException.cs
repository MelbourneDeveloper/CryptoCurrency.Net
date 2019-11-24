using System;

namespace CryptoCurrency.Net.Base.AddressManagement.BCH
{
    [Serializable]
    public class CashAddrConversionException : Exception
    {
        public CashAddrConversionException()
            : base()
        {
        }
        public CashAddrConversionException(string message)
            : base(message)
        {
        }
    }
}