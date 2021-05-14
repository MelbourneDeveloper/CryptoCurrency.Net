namespace Hardwarewallets.Net.Model
{
    public interface ITransaction
    {
        IAddressPath From { get; }
        decimal Value { get; }
        string To { get; }
    }
}
