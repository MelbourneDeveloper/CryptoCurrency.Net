using CryptoCurrency.Net.APIClients;
using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.UnitTests
{
    [TestClass]
    public class BlockchainTests
    {
        private static readonly BlockchainClientManager _BlockchainClientManager = new BlockchainClientManager(new RESTClientFactory());

        [TestMethod]
        public async Task GetEthereumAddresses()
        {
            var addresses = new List<string>
                {
                    "0x0E95F8F8ecBd770585766c1CD216C81aA43439a6",
                    "0xda4D788FA55CDE88C8dc93Ceb4Ce9EDCf26Ee2A5",
                    "0x26769f254f1Ba073cEF6e9E47a7320332a4dA3D8"
                    //TODO: Reenable these. They break issue: https://github.com/MelbourneDeveloper/CryptoCurrency.Net/issues/1
                    //"0xFBb1b73C4f0BDa4f67dcA266ce6Ef42f520fBB98",
                    //"0xfCA70E67b3f93f679992Cd36323eEB5a5370C8e4"
                };

            var addressInformations = (await _BlockchainClientManager.GetAddresses(CurrencySymbol.Ethereum, addresses)).ToList();

            var ethereumResults = addressInformations.FirstOrDefault(a => a.Key.Equals(CurrencySymbol.Ethereum));

            var expected = ethereumResults.Value.Count();
            Assert.AreEqual(expected, addresses.Count, "The number of addresses returned does not match the number requested.");
        }

        [TestMethod]
        public async Task GetBitcoinGoldAddresses()
        {
            await TestCoin(CurrencySymbol.BitcoinGold, new List<string> { "GJjz2Du9BoJQ3CPcoyVTHUJZSj62i1693U", "GJjz2Du9BoJQ3CPcoyVTHUJZSj62i1693U" });
        }

        [TestMethod]
        public async Task GetZCashAddresses()
        {
            await TestCoin(CurrencySymbol.ZCash, new List<string> { "t1e8F9YdPuNKv8JjWspXPxoP8ZJADtwKdQs", "t3fJZ5jYsyxDtvNrWBeoMbvJaQCj4JJgbgX" });
        }

        [TestMethod]
        public async Task GetBitcoinCashAddresses()
        {
            await TestCoin(CurrencySymbol.BitcoinCash, new List<string> { "qzl8jth497mtckku404cadsylwanm3rfxsx0g38nwlqzl8jth497mtckku404cadsylwanm3rfxsx0g38nwl", "bitcoincash:qrcuqadqrzp2uztjl9wn5sthepkg22majyxw4gmv6p" });
        }

        [TestMethod]
        public async Task GetBitcoinAddresses()
        {
            await TestCoin(CurrencySymbol.Bitcoin, new List<string> { "32SrnYR7PTJKsjXcHpD2CeQFzWT4XpPtnv" });
        }

        [TestMethod]
        public async Task GetDashTransactions()
        {
            var blockExplorerClient = new DashClient(CurrencySymbol.Dash, new RESTClientFactory());
            var transactionsAtAddress = await blockExplorerClient.GetTransactionsAtAddress("XdAUmwtig27HBG6WfYyHAzP8n6XC9jESEw");
            Assert.IsNotNull(transactionsAtAddress, "No result was returned");
            Assert.IsTrue(transactionsAtAddress.Transactions.Count > 0, "No transactions were returned");
            foreach (var transaction in transactionsAtAddress.Transactions)
            {
                var inputsValue = transaction.Inputs.Sum(i => i.Amount);
                var outputsValue = transaction.Outputs.Sum(o => o.Amount);
                Assert.AreEqual(inputsValue, outputsValue, "The inputs total doesn't match the outputs total.");
            }
        }

        private static async Task TestCoin(CurrencySymbol symbol, IReadOnlyCollection<string> addresses2)
        {
            var blockchainClientManager = new BlockchainClientManager(new RESTClientFactory());

            for (var i = 0; i < 10; i++)
            {
                var addressDictionary = await blockchainClientManager.GetAddresses(symbol, addresses2);

                foreach (var key in addressDictionary.Keys)
                {
                    var addresses = addressDictionary[key];
                    foreach (var address in addresses)
                        Assert.IsTrue(address.IsUnused.HasValue || address.TransactionCount.HasValue, "Can't tell if the address has transactions");
                }
            }
        }
    }
}
