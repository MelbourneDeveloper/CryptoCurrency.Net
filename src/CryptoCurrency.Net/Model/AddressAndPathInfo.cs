namespace CryptoCurrency.Net.Model
{
    public class AddressAndPathInfo
    {
        public uint Index { get; set; }
        public bool IsChange { get; set; }
        public string Address { get; set; }

        public AddressAndPathInfo(uint index, bool isChange, string address)
        {
            Index = index;
            IsChange = isChange;
            Address = address;
        }
    }
}
