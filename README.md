# CryptoCurrency.Net
Cross platform C# library for general Crypto Currency functionality, communicating with Cryptocurrency exchanges, and Blockchain APIs.

This library is designed for any project that works with cryptocurrency in any way. It attempts to put a layer over several aspects of querying the blockchain such as getting address balances, transactions and so on.

Join us on Slack:
https://hardwarewallets.slack.com

Twitter:
https://twitter.com/HardfolioApp

Blog:
https://christianfindlay.wordpress.com

Currently supports:
* .NET Framework
* .NET Core
* Android
* UWP 

## Quick Start

- Clone the repo and open the solution
- Compile and run one of the unit tests

Or...

Install the NuGet and use the example code.

```cs
        //Output: Address: qzl8jth497mtckku404cadsylwanm3rfxsx0g38nwlqzl8jth497mtckku404cadsylwanm3rfxsx0g38nwl Balance: 0
        public async Task GetBitcoinCashAddresses()
        {
            var blockchainClientManager = new BlockchainClientManager(new RESTClientFactory());
            var addressDictionary = await blockchainClientManager.GetAddresses(CurrencySymbol.BitcoinCash,
            new List<string> {
            "qzl8jth497mtckku404cadsylwanm3rfxsx0g38nwlqzl8jth497mtckku404cadsylwanm3rfxsx0g38nwl",
            "bitcoincash:qrcuqadqrzp2uztjl9wn5sthepkg22majyxw4gmv6p" });
            var address = addressDictionary[CurrencySymbol.BitcoinCash].First();
            Console.WriteLine(
            $"Address: {address.Address} Balance: { address.Balance }"
            );
        }
```


```cs
        //Output: Token: RHOC Balance: 0.49048
        public async Task GetERC20Tokens()
        {
            var result = await _BlockchainClientManager.GetAddresses(CurrencySymbol.Ethereum, 
            new List<string> { "0xA3079895DD50D9dFE631e8f09F3e3127cB9a4970" });
            var nonEthereumResult = result.FirstOrDefault(a => !a.Key.Equals(CurrencySymbol.Ethereum));
            Console.WriteLine(
            $"Token: {nonEthereumResult.Key} Balance: {nonEthereumResult.Value.First().Balance}");
        }
```

```cs
        //Output:
        //Currency: ETH Balance: 0.????????
        //Currency: BNB Balance: 0.????????
        //Currency: EOS Balance: 0.????????
        //public async Task GetBinanceAddresses()
        {
            var binanceClient = new BinanceClient(ApiKey, ApiSecret, new RESTClientFactory());
            var holdings = await binanceClient.GetHoldings(binanceClient);
            foreach(var holding in holdings.Result)
            {
                Console.WriteLine($"Currency: {holding.Symbol} Balance: {holding.HoldingAmount}");
            }
        }
```


## NuGet

Install-Package CryptoCurrency.Net

## Contribution

I welcome feedback, and pull requests. If there's something that you need to change in the library, please log an issue, and explain the problem. If you have a proposed solution, please write it up and explain why you think it is the answer to the problem. The best way to highlight a bug is to submit a pull request with a unit test that fails so I can clearly see what the problem is in the first place.

### Pull Requests

Please break pull requests up in to their smallest possible parts. If you have a small feature of refactor that other code depends on, try submitting that first. Please try to reference an issue so that I understand the context of the pull request. If there is no issue, I don't know what the code is about. If you need help, please jump on Slack here: https://hardwarewallets.slack.com

## Donate

All my libraries are open source and free. Your donations will contribute to making sure that these libraries keep up with the latest blockahin APIs, hardwarewallet firmware, and functions are implemented, and the quality is maintained.

Bitcoin: 33LrG1p81kdzNUHoCnsYGj6EHRprTKWu3U

Ethereum: 0x7ba0ea9975ac0efb5319886a287dcf5eecd3038e

Litecoin: MVAbLaNPq7meGXvZMU4TwypUsDEuU6stpY

## Store App Production Usage

This app currently only Supports Trezor (https://github.com/MelbourneDeveloper/Trezor.Net) but it will soon support Ledger with this library.

https://play.google.com/store/apps/details?id=com.Hardfolio (Android)

https://www.microsoft.com/en-au/p/hardfolio/9p8xx70n5d2j (UWP)

## See Also

[Trezor.Net](https://github.com/MelbourneDeveloper/Trezor.Net) - Trezor Hardwarewallet Library

[Ledger.Net](https://github.com/MelbourneDeveloper/Ledger.Net) - Ledger Hardwarewallet Library

[KeepKey.Net](https://github.com/MelbourneDeveloper/KeepKey.Net) - KeepKey Hardwarewallet Library



