using CryptoCurrency.Net.APIClients;
using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.UnitTests
{
    [TestClass]
    public class BlockchainTests
    {
        private readonly string ApiSecret = string.Empty;
        private readonly string ApiKey = string.Empty;
        private static readonly BlockchainClientManager _BlockchainClientManager = new BlockchainClientManager(new RESTClientFactory());

        //Output: Address: qzl8jth497mtckku404cadsylwanm3rfxsx0g38nwlqzl8jth497mtckku404cadsylwanm3rfxsx0g38nwl Balance: 0
        public async Task GetBitcoinCashAddressesVerbose()
        {
            var blockchainClientManager = new BlockchainClientManager(new RESTClientFactory());
            var addresses = await blockchainClientManager.GetAddresses(CurrencySymbol.BitcoinCash,
            new List<string> {
            "qzl8jth497mtckku404cadsylwanm3rfxsx0g38nwlqzl8jth497mtckku404cadsylwanm3rfxsx0g38nwl",
            "bitcoincash:qrcuqadqrzp2uztjl9wn5sthepkg22majyxw4gmv6p" });
            var address = addresses[CurrencySymbol.BitcoinCash].First();
            Console.WriteLine(
            $"Address: {address.Address} Balance: { address.Balance }"
            );
        }

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
        public async Task GetERC20Tokens()
        {
            var result = await _BlockchainClientManager.GetAddresses(CurrencySymbol.Ethereum, new List<string> { "0xA3079895DD50D9dFE631e8f09F3e3127cB9a4970" });
            var nonEthereumResult = result.FirstOrDefault(a => !a.Key.Equals(CurrencySymbol.Ethereum));
            Console.WriteLine($"Token: {nonEthereumResult.Key} Balance: {nonEthereumResult.Value.First().Balance}");
        }


        [TestMethod]
        public async Task GetCardanoAddresses()
        {
            await TestCoin(CurrencySymbol.Cardano, new List<string> { "DdzFFzCqrht1jU5aJCnkX2ZuaQbEEdoDQ3f5K6MYXvekgG8MyDWtpJwHmV7q1wxfdSTe3bUDxsAR6MZ3pUzGeWoWBuHATsXFxRg4etZu", "DdzFFzCqrhstM8aPuFQvUTkxV3sF4GBW8Ju6ZCDK6hJZE9bsW7fZ8JULxhoeRXdPTp5DnnbwiBhqsMY5eiD4xMovrxAuqkjb51S2Kgwt" });
        }

        [TestMethod]
        public async Task GetBitcoinGoldAddresses()
        {
            await TestCoin(CurrencySymbol.BitcoinGold, new List<string> { "GJjz2Du9BoJQ3CPcoyVTHUJZSj62i1693U", "GJjz2Du9BoJQ3CPcoyVTHUJZSj62i1693U" });
        }

        [TestMethod]
        public async Task GetLitecoinAddresses()
        {
            await TestCoin(CurrencySymbol.Litecoin, new List<string> { "LUcxeeZVoohbkkEMY2s6LmEXu9nMcL2rAS", "LSs49i5VEV57wUEeVrsrzwHwJCLx8uDMva" });
        }

        [TestMethod]
        public async Task GetTronAddresses()
        {
            await TestCoin(CurrencySymbol.Tron, new List<string> { "TMuA6YqfCeX8EhbfYEg5y7S4DqzSJireY9", "TWd4WrZ9wn84f5x1hZhL4DHvk738ns5jwb" }, 3);
        }

        [TestMethod]
        public async Task GetZCashAddresses()
        {
            await TestCoin(CurrencySymbol.ZCash, new List<string> { "t1e8F9YdPuNKv8JjWspXPxoP8ZJADtwKdQs", "t3fJZ5jYsyxDtvNrWBeoMbvJaQCj4JJgbgX" });
        }

        [TestMethod]
        public async Task GetBitcoinCashAddresses()
        {
            await TestCoin(CurrencySymbol.BitcoinCash, new List<string> { BCH.AddressConverter.ToNewFormat("15urYnyeJe3gwbGJ74wcX89Tz7ZtsFDVew").Address, "qrcuqadqrzp2uztjl9wn5sthepkg22majyxw4gmv6p" });
        }

        [TestMethod]
        public async Task GetEthereumClassicAddresses()
        {
            await TestCoin(CurrencySymbol.EthereumClassic, new List<string>
            {
                "0x4afaf9ba702636dd05d633dff7e2f0fe652c1375",
                "0xaaba597a965c781fc66dc93a32c371eccc0cccff"

                //TODO: Reeable to test lots of transactions
                //"0xDd25785b55d988aafD0B8FA1eFcdbb4d6178ab01",
                //TODO: Reeanble to test big numbers
                //"0x6667ED6CB6E7aCCc4004E8844dBdd0E72D58c31C"
            });
        }

        //[TestMethod]
        //public async Task GetBinanceAddresses()
        //{
        //    var binanceClient = new BinanceClient(ApiKey, ApiSecret, new RESTClientFactory());
        //    var holdings = await binanceClient.GetHoldings(binanceClient);
        //    foreach (var holding in holdings.Result)
        //    {
        //        Console.WriteLine($"Currency: {holding.Symbol} Balance: {holding.HoldingAmount}");
        //    }
        //}

        [TestMethod]
        public async Task GetBitcoinAddresses()
        {
            await TestCoin(CurrencySymbol.Bitcoin, new List<string> { "32SrnYR7PTJKsjXcHpD2CeQFzWT4XpPtnv" });
        }

        [TestMethod]
        public async Task GetDashTransactions()
        {
            var blockExplorerClient = new DashClient(CurrencySymbol.Dash, new RESTClientFactory());
            var transactionsAtAddress = await blockExplorerClient.GetTransactionsAtAddress("XuSnty6UFqtohMRVf3NxUDh64LqwnxptwA");
            Assert.IsNotNull(transactionsAtAddress, "No result was returned");
            Assert.IsTrue(transactionsAtAddress.Transactions.Count > 0, "No transactions were returned");
            foreach (var transaction in transactionsAtAddress.Transactions)
            {
                var inputsValue = transaction.Inputs.Sum(i => i.Value);
                var outputsValue = transaction.Outputs.Sum(o => o.Value);

                var difference = inputsValue - outputsValue;

                Assert.AreEqual(difference, transaction.Fees);

                Assert.AreEqual(inputsValue, outputsValue + transaction.Fees, "The inputs total doesn't match the outputs total.");
            }
        }

        private static async Task TestCoin(CurrencySymbol symbol, List<string> inputAddresses, int repeatCount = 10)
        {
            var blockchainClientManager = new BlockchainClientManager(new RESTClientFactory());

            for (var i = 0; i < repeatCount; i++)
            {
                var addressDictionary = await blockchainClientManager.GetAddresses(symbol, inputAddresses);

                foreach (var key in addressDictionary.Keys)
                {
                    var outputAddresses = addressDictionary[key].ToList();
                    foreach (var address in outputAddresses)
                    {
                        Assert.AreEqual(inputAddresses.Count, outputAddresses.Count, "The number of addresses returned was not the same as the number of addresses called");

                        for (var x = 0; x < inputAddresses.Count; x++)
                        {
                            Assert.AreEqual(inputAddresses[x].ToLower(), outputAddresses[x].Address.ToLower(), "An inputted address turned out to be different to the outputted address");
                        }

                        Assert.IsTrue(address.IsUnused.HasValue || address.TransactionCount.HasValue, "Can't tell if the address has transactions");

                        Console.WriteLine($"Address: {address.Address} Balance: {address.Balance} Transaction Count: {address.TransactionCount} Is Unused: {address.IsUnused}");
                    }
                }
            }
        }
    }
}
