﻿namespace CryptoCurrency.Net.APIClients.Model.BTCMarkets
{
    public class Balance
    {
        public decimal balance { get; set; }
        public decimal pendingFunds { get; set; }
        public string currency { get; set; }
    }
}
