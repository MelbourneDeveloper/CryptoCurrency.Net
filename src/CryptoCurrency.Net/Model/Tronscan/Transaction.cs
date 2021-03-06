﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCurrency.Net.Model.Tronscan
{
    public class ContractData
    {
        public object amount { get; set; }
        public string asset_name { get; set; }
        public string owner_address { get; set; }
        public string to_address { get; set; }
    }

    public class Datum
    {
        public int block { get; set; }
        public string hash { get; set; }
        public object timestamp { get; set; }
        public string ownerAddress { get; set; }
        public string toAddress { get; set; }
        public int contractType { get; set; }
        public bool confirmed { get; set; }
        public ContractData contractData { get; set; }
        public string SmartCalls { get; set; }
        public string Events { get; set; }
        public string id { get; set; }
        public string data { get; set; }
        public string fee { get; set; }
    }

    public class Transaction
    {
        public int total { get; set; }
        public int rangeTotal { get; set; }
        public List<Datum> data { get; set; }
    }
}
