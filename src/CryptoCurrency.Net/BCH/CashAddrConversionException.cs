using System;

namespace CryptoCurrency.Net.BCH
{
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