using CryptoCurrency.Net.Abstractions.AddressManagement;
using CryptoCurrency.Net.AddressManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Hardwarewallets.Net.UnitTests
{
    public abstract class UnitTestBase
    {
        //TODO: Unit tests have been disabled until the latest changes have been rolled in to all libraries.

        public static IAddressDeriver HardwarewalletManager { get; protected set; }
        public abstract Task Initialize();

        //[Fact]
        //public async Task GetBitcoinAddress()
        //{
        //    await Initialize();
        //    var address = await AddressDeriver.GetAddressAsync(AddressPathBase.Parse<BIP44AddressPath>("49/0/0/0/0"), false, true);
        //}

        //[Fact]
        //public async Task GetBitcoinAddresses()
        //{
        //    await Initialize();

        //    var addressManager = new AddressManager(AddressDeriver, new BIP44AddressPathFactory(true, 0));

        //    //Get 10 addresses with all the trimming
        //    const int numberOfAddresses = 3;
        //    const int numberOfAccounts = 2;
        //    var addresses = await addressManager.GetAddressesAsync(0, numberOfAddresses, numberOfAccounts, true, true);

        //    Assert.IsTrue(addresses != null);
        //    Assert.IsTrue(addresses.Accounts != null);
        //    Assert.IsTrue(addresses.Accounts.Count == numberOfAccounts);
        //    Assert.IsTrue(addresses.Accounts[0].Addresses.Count == numberOfAddresses);
        //    Assert.IsTrue(addresses.Accounts[1].Addresses.Count == numberOfAddresses);
        //    Assert.IsTrue(addresses.Accounts[0].ChangeAddresses.Count == numberOfAddresses);
        //    Assert.IsTrue(addresses.Accounts[1].ChangeAddresses.Count == numberOfAddresses);
        //}

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
