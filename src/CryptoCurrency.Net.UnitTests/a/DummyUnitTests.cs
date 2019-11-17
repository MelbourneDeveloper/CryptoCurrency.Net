using CryptoCurrency.Net.AddressManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Hardwarewallets.Net.UnitTests
{
    [TestClass]
    public class DummyUnitTests : UnitTestBase
    {
        public override async Task Initialize()
        {
            HardwarewalletManager = new DummyWalletManager();
        }

        [TestMethod]
        public async Task GetBitcoinPublicKey()
        {
            Initialize();
            var publicKey = await HardwarewalletManager.GetAddressAsync(new BIP44AddressPath(true, 0, 0, false, 0), true, true);
            Assert.AreEqual("02a1633cafcc01ebfb6d78e39f687a1f0995c62fc95f51ead10a02ee0be551b5dc", publicKey);
        }

        [TestMethod]
        public async Task GetEthereumAddress()
        {
            Initialize();
            var address = await HardwarewalletManager.GetAddressAsync(new BIP44AddressPath(true, 60, 0, false, 0), false, true);
            Assert.AreEqual("0x3f2dD9850509367b57C900F7e1C5f4F0bfF1014B", address);
        }

        [TestMethod]
        public async Task GetEthereumPublicKey()
        {
            Initialize();
            var publicKey = await HardwarewalletManager.GetAddressAsync(new BIP44AddressPath(true, 60, 0, false, 0), true, true);
            Assert.AreEqual("0x3f2dD9850509367b57C900F7e1C5f4F0bfF1014Bf4F0bfF1014B", publicKey);
        }

        //[Fact]
        //public async Task SignEthereumTransaction()
        //{
        //    var signedTransaction = await HardwarewalletManager.SignTransaction<IEthereumTransaction, ISignedEthereumTransaction>(new DummyEthereumTransaction());
        //    Assert.Equal(32, signedTransaction.SignatureR.Length);
        //    Assert.Equal(32, signedTransaction.SignatureS.Length);
        //}

    }
}
