using System.Collections.Generic;

namespace CryptoCurrency.Net.Model.SomeClient2
{
    public class Address
    {
        public int n_tx { get; set; }
        public int final_balance { get; set; }
        public string address { get; set; }
    }

    public class AddressResult
    {
        public List<Address> addresses { get; } = new List<Address>();
    }
}
