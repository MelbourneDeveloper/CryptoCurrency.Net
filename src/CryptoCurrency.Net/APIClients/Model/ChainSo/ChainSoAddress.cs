namespace CryptoCurrency.Net.APIClients.Model.ChainSo
{
    public class Datum
    {
        public decimal confirmed_balance { get; set; }
    }

    public class ChainSoAddress
    {
        public Datum data { get; set; }
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
