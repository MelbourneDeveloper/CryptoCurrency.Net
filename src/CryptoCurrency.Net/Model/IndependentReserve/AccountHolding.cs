using System;

namespace CryptoCurrency.Net.Model.IndependentReserve
{
    public class AccountHolding
    {
        public Guid AccountGuid { get; set; }
        public string AccountStatus { get; set; }
        public decimal AvailableBalance { get; set; }
        public string CurrencyCode { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
