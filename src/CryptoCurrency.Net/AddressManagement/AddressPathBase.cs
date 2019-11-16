using CryptoCurrency.Net.Abstractions.AddressManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoCurrency.Net.AddressManagement
{
    public abstract class AddressPathBase : IAddressPath
    {
        #region Public Properties
        public List<IAddressPathElement> AddressPathElements { get; private set; } = new List<IAddressPathElement>();
        #endregion

        #region Private Static Methods
        private static AddressPathElement ParseElement(string elementString)
        {
            if (!uint.TryParse(elementString.Replace("'", string.Empty), out var unhardenedNumber))
            {
                throw new ParseAddressPathException($"The value {elementString} is not a valid path element");
            }

            return new AddressPathElement { Harden = elementString.EndsWith("'"), Value = unhardenedNumber };
        }
        #endregion

        #region Public Methods
        public uint[] ToArray() => AddressPathElements.Select(ape => ape.Harden ? AddressUtilities.HardenNumber(ape.Value) : ape.Value).ToArray();
        #endregion

        public override string ToString()
        {
            return $"m/{string.Join("/", AddressPathElements.Select(ape=>$"{ape.Value}{(ape.Harden?"'":string.Empty)}"))}"; 
        }

        #region Public Static Methods
        public static T Parse<T>(string path) where T : AddressPathBase, new()
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            return new T
            {
                AddressPathElements = path.Split('/')
                .Where(t => string.Compare("m", t, StringComparison.OrdinalIgnoreCase) != 0)
                .Select(ParseElement)
                .Cast<IAddressPathElement>()
                .ToList()
            };
        }
        #endregion
    }
}
