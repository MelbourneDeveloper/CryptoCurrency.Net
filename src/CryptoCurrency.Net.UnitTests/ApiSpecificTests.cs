using CryptoCurrency.Net.APIClients;
using CryptoCurrency.Net.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoCurrency.Net.Ethereum;
using System.Linq;

namespace CryptoCurrency.Net.UnitTests
{
    [TestClass]
    public class ApiSpecificTests
    {
        [TestMethod]
        public async Task TestEtherscanGetAddress()
        {
            var etherscanClient = new EtherscanClient(CurrencySymbol.Ethereum, new RESTClientFactory());
            var emptyAddress = "0x0E95F8F8ecBd770585766c1CD216C81aA43439a7".ToLower();
            var balances = await etherscanClient.GetAddresses(new List<string>
                {
                    emptyAddress,
                    "0x0E95F8F8ecBd770585766c1CD216C81aA43439a6",
                    "0xda4D788FA55CDE88C8dc93Ceb4Ce9EDCf26Ee2A5",
                    "0x26769f254f1Ba073cEF6e9E47a7320332a4dA3D8"
                });

            //Check that the empty balance account is empty
            var emptyBalance = balances.First(a => a.Address == emptyAddress);
            Assert.AreEqual(true, emptyBalance.IsUnused);
            Assert.AreEqual(0, emptyBalance.Balance);

            //Check that there are 4
            Assert.AreEqual(4, balances.ToList().Count);

            //Check that the other three are not unused
            Assert.AreEqual(3, balances.Where(b => !b.IsUnused.Value).Count());
        }

        [TestMethod]
        public async Task TestConcurrentEtherscanGetAddress()
        {
            var etherscanClient = new EtherscanClient(CurrencySymbol.Ethereum, new RESTClientFactory());
            var tasks = new List<Task<IEnumerable<BlockChainAddressInformation>>>();

            for (var i = 0; i < 10; i++)
            {
                tasks.Add(etherscanClient.GetAddresses(new List<string>
                {
                    "0x0E95F8F8ecBd770585766c1CD216C81aA43439a6",
                    "0xda4D788FA55CDE88C8dc93Ceb4Ce9EDCf26Ee2A5",
                    "0x26769f254f1Ba073cEF6e9E47a7320332a4dA3D8"
                }));
            }

            var results = await Task.WhenAll(tasks);

            Assert.AreEqual(10, results.Length);
        }

        [TestMethod]
        public void TestToEthereumBalance()
        {
            const string wei = "399395627285559445";
            var ether = wei.ToEthereumBalance();
            Assert.AreEqual(decimal.Parse($"0.{wei}"), ether);
        }

    }
}
