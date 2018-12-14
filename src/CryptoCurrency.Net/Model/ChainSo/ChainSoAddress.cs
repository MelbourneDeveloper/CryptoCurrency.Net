namespace CryptoCurrency.Net.Model.ChainSo
{
    public class Data
    {
        public decimal confirmed_balance { get; set; }
    }

    public class ChainSoAddress
    {
        public Data data { get; set; }
    }


    public class ReceivedData
    {
        public decimal confirmed_received_value { get; set; }
    }

    public class ChainSoAddressReceived
    {
        public ReceivedData data { get; set; }
    }

}
