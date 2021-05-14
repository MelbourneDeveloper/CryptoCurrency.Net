using CryptoCurrency.Net.Base.Abstractions.AddressManagement;

namespace CryptoCurrency.Net.Base.AddressManagement
{
    public class BIP44AddressPath : AddressPathBase, IBIP44AddressPath
    {
        public uint Purpose => Validate() ? AddressPathElements[0].Value : 0;

        public uint CoinType => Validate() ? AddressPathElements[1].Value : 0;

        public uint Account => Validate() ? AddressPathElements[2].Value : 0;

        public uint Change => Validate() ? AddressPathElements[3].Value : 0;

        public uint AddressIndex => Validate() ? AddressPathElements[4].Value : 0;

        public BIP44AddressPath()
        {
        }

        public bool Validate()
        {
            var errorPrefix = "The address path is not a valid BIP44 Address Path.";
            return AddressPathElements.Count != 5
                ? throw new AddressPathException($"{errorPrefix} 5 Elements are required but {AddressPathElements.Count} were found.")
                : !AddressPathElements[0].Harden
                ? throw new AddressPathException($"{errorPrefix} Purpose must be hardened")
                : AddressPathElements[0].Value is not 44 and not 49
                ? throw new AddressPathException($"{errorPrefix} Purpose must 44 or 49")
                : !AddressPathElements[1].Harden
                ? throw new AddressPathException($"{errorPrefix} Coint Type must be hardened")
                : !AddressPathElements[2].Harden
                ? throw new AddressPathException($"{errorPrefix} Account must be hardened")
                : AddressPathElements[3].Harden
                ? throw new AddressPathException($"{errorPrefix} Change must not be hardened")
                : AddressPathElements[3].Value is not 0 and not 1
                ? throw new AddressPathException($"{errorPrefix} Change must 0 or 1")
                : AddressPathElements[4].Harden ? throw new AddressPathException($"{errorPrefix} Address Index must not be hardened") : true;
        }

        public BIP44AddressPath(bool isSegwit, uint coinType, uint account, bool isChange, uint addressIndex)
        {
            AddressPathElements.Add(new AddressPathElement { Value = isSegwit ? (uint)49 : 44, Harden = true });
            AddressPathElements.Add(new AddressPathElement { Value = coinType, Harden = true });
            AddressPathElements.Add(new AddressPathElement { Value = account, Harden = true });
            AddressPathElements.Add(new AddressPathElement { Value = isChange ? (uint)1 : 0, Harden = false });
            AddressPathElements.Add(new AddressPathElement { Value = addressIndex, Harden = false });
        }
    }
}
