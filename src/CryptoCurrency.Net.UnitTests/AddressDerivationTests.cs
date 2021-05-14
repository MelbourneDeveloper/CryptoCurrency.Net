using CryptoCurrency.Net.Base.Abstractions.AddressManagement;
using CryptoCurrency.Net.Base.AddressManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable IDE0058 // Expression value is never used

namespace CryptoCurrency.Net.UnitTests
{
    [TestClass]
    public class AddressDerivationTests
    {
        //TODO: Unit tests have been disabled until the latest changes have been rolled in to all libraries.

        public static IAddressDeriver AddressDeriver { get; } = new DummyAddressDeriver();

        [TestMethod]
        public async Task GetBitcoinPublicKey()
        {
            var publicKey = await AddressDeriver.GetAddressAsync(new BIP44AddressPath(true, 0, 0, false, 0), true, true);
            Assert.AreEqual("02a1633cafcc01ebfb6d78e39f687a1f0995c62fc95f51ead10a02ee0be551b5dc", publicKey);
        }

        [TestMethod]
        public async Task GetEthereumAddress()
        {
            var address = await AddressDeriver.GetAddressAsync(new BIP44AddressPath(true, 60, 0, false, 0), false, true);
            Assert.AreEqual("0x3f2dD9850509367b57C900F7e1C5f4F0bfF1014B", address);
        }

        [TestMethod]
        public async Task GetEthereumPublicKey()
        {
            var publicKey = await AddressDeriver.GetAddressAsync(new BIP44AddressPath(true, 60, 0, false, 0), true, true);
            Assert.AreEqual("0x3f2dD9850509367b57C900F7e1C5f4F0bfF1014Bf4F0bfF1014B", publicKey);
        }

        [TestMethod]
        public void TestParser()
        {
            var pathAsString = "m/45/5/3/5'/0'";
            var customAddressPath = AddressPathBase.Parse<CustomAddressPath>(pathAsString);

            Assert.AreEqual(pathAsString, customAddressPath.ToString());

            Assert.IsTrue(5 == customAddressPath.AddressPathElements.Count);

            Assert.IsTrue(45 == customAddressPath.AddressPathElements[0].Value);
            Assert.IsFalse(customAddressPath.AddressPathElements[0].Harden);

            Assert.IsTrue(5 == customAddressPath.AddressPathElements[1].Value);
            Assert.IsFalse(customAddressPath.AddressPathElements[1].Harden);

            Assert.IsTrue(3 == customAddressPath.AddressPathElements[2].Value);
            Assert.IsFalse(customAddressPath.AddressPathElements[2].Harden);

            Assert.IsTrue(5 == customAddressPath.AddressPathElements[3].Value);
            Assert.IsTrue(customAddressPath.AddressPathElements[3].Harden);

            Assert.IsTrue(2147483653 == customAddressPath.ToArray()[3]);

            pathAsString = "m/45/5/3/5'";
            customAddressPath = AddressPathBase.Parse<CustomAddressPath>(pathAsString);
            Assert.AreEqual(pathAsString, customAddressPath.ToString());

            Assert.IsTrue(2147483653 == customAddressPath.ToArray()[3]);
            Assert.IsTrue(5 == customAddressPath.AddressPathElements[3].Value);

            customAddressPath = AddressPathBase.Parse<CustomAddressPath>("0/1/2");

            Assert.IsTrue(0 == customAddressPath.AddressPathElements[0].Value);
            Assert.IsTrue(1 == customAddressPath.AddressPathElements[1].Value);
            Assert.IsTrue(2 == customAddressPath.AddressPathElements[2].Value);

            var bip44AddressPath = AddressPathBase.Parse<BIP44AddressPath>("44'/0'/0'/0/0");

            Assert.IsTrue(44 == bip44AddressPath.Purpose);
            Assert.IsTrue(0 == bip44AddressPath.CoinType);
            Assert.IsTrue(0 == bip44AddressPath.Account);
            Assert.IsTrue(0 == bip44AddressPath.Change);
            Assert.IsTrue(0 == bip44AddressPath.AddressIndex);

            Assert.IsTrue(bip44AddressPath.ToArray()[0] == 2147483692);

            var sb = new StringBuilder();
            sb.Append("m");
            for (var i = 0; i < 100; i++)
            {
                sb.Append($"/{i}{(i % 2 == 0 ? "'" : string.Empty)}");
            }
            customAddressPath = AddressPathBase.Parse<CustomAddressPath>(sb.ToString());
            for (var i = 0; i < 100; i++)
            {
                Assert.IsTrue(customAddressPath.AddressPathElements[i].Harden == (i % 2 == 0));
            }

            Assert.AreEqual(sb.ToString(), customAddressPath.ToString());

            Exception validationException = null;
            try
            {
                bip44AddressPath = AddressPathBase.Parse<BIP44AddressPath>("44'/0'/0/0/0");
                bip44AddressPath.Validate();
            }
            catch (Exception ex)
            {
                validationException = ex;
            }
            Assert.IsNotNull(validationException);

            validationException = null;
            try
            {
                bip44AddressPath = AddressPathBase.Parse<BIP44AddressPath>("50'/0'/0'/0/0");
                bip44AddressPath.Validate();
            }
            catch (Exception ex)
            {
                validationException = ex;
            }
            Assert.IsNotNull(validationException);
        }
    }
}
