using CryptoCurrency.Net.APIClients.BlockchainClients;
using CryptoCurrency.Net.APIClients.BlockchainClients.CallArguments;
using CryptoCurrency.Net.Model;
using CryptoCurrency.Net.Model.JSONRPC;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.APIClients
{
    public abstract class JSONRPCClientBase : BlockchainClientBase, ITokenClient
    {
        private static readonly List<TokenInfo> Tokens = new List<TokenInfo>
        {
            new TokenInfo("EOS", "0x86fa049857e0209aa7d9e616f7eb3b3b78ecfdb0", 18),
            new TokenInfo("TRX", "0xf230b790e05390fc8295f4d3f60332c93bed42e2", 6),
            new TokenInfo("VEN", "0xd850942ef8811f2a866692a623011bde52a462c1", 18),
            new TokenInfo("BNC", "0x14bcce8e9d9ab3445eeeceb0d5ee6c0286efdc77", 8),
            new TokenInfo("EOS", "0x86fa049857e0209aa7d9e616f7eb3b3b78ecfdb0", 18),
            new TokenInfo("ZIL", "0x05f4a42e251f2d52b8ed15e9fedaacfcef1fad27", 12),
            new TokenInfo("AE",  "0x5ca9a71b1d01849c0a95490cc00559717fcf0d1d", 18),
            new TokenInfo("BTM", "0xcb97e65f07da24d46bcdd078ebebd7c6e6e3d750", 8),
            new TokenInfo("ICX", "0xb5a5f22694352c15b00323844ad545abb2b11028", 18),
            new TokenInfo("REP", "0xe94327d07fc17907b4db788e5adf2ed424addff6", 18),
            new TokenInfo("RHOC", "0x168296bb09e24a88805cb9c33356536b980d3fc5", 8),
            new TokenInfo("MKR", "0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2", 18),
            new TokenInfo("ZRX", "0xe41d2489571d322189246dafa5ebde1f4699f498", 18),
            new TokenInfo("GTN", "0xa74476443119A942dE498590Fe1f2454d7D4aC0d", 18)
        };

        #region Constructor
        protected JSONRPCClientBase(CurrencySymbol currency, IClientFactory restClientFactory) : base(currency, restClientFactory)
        {
            if (restClientFactory == null) throw new ArgumentNullException(nameof(restClientFactory));
            RESTClient = (Client)RESTClientFactory.CreateClient(BaseUriPath);
            Currency = currency;
        }
        #endregion

        #region Protected Overridable Properties
        protected abstract Uri BaseUriPath { get; }
        #endregion

        #region Func

        protected override Func<GetAddressesArgs, Task<IEnumerable<BlockChainAddressInformation>>> GetAddressesFunc { get; } = async getAddressesArgs =>
        {
            var retVal = new List<BlockChainAddressInformation>();

            var requests = new List<RequestBase>();

            var addresses = getAddressesArgs.Addresses.ToList();

            for (var i = 0; i < addresses.Count; i++)
            {
                var address = addresses[i];
                requests.Add(new RequestBase { Method = Method.eth_getBalance, Id = i * 2, Params = { address.ToLower(), DefaultBlock.latest.ToString() } });
                requests.Add(new RequestBase { Method = Method.eth_getTransactionCount, Id = (i * 2) + 1, Params = { address.ToLower(), DefaultBlock.latest.ToString() } });
            }

            var client = (JSONRPCClientBase)getAddressesArgs.Client;
            var balanceResults = (await client.PostAsync(requests)).ToList();

            balanceResults = balanceResults.OrderBy(br => br.Id).ToList();

            for (var i = 0; i < balanceResults.Count; i += 2)
            {
                var balanceResult = balanceResults[i];

                var request = requests[i];

                if (balanceResult.Error != null)
                {
                    throw new GetAddressException($"Error getting address: {request.Params[0]}.\r\nCode: {balanceResult.Error.Code} Message: {balanceResult.Error.Message}");
                }

                var balance = GetEthFromHex(balanceResult.Result);
                var transactionCount = GetLongFromHex(balanceResults[i + 1].Result);

                retVal.Add(new BlockChainAddressInformation(request.Params[0].ToString(), balance, balance == 0 && transactionCount == 0));
            }

            return retVal;
        };

        private static decimal GetEthFromHex(string weiHex)
        {
            return GetLongFromHex(weiHex) / CurrencySymbol.Wei;
        }

        //TODO Long is no good here. Need a BigInteger
        private static long GetLongFromHex(string weiHex)
        {
            return long.Parse(weiHex.Substring(2, weiHex.Length - 2), NumberStyles.HexNumber);
        }

        public override async Task<BlockChainAddressInformation> GetAddress(string address)
        {
            await Task.Delay(1);
            throw new GetAddressException($"{nameof(GetAddressesFunc)} should be used.");
        }

        public async Task<IEnumerable<ResultBase>> PostAsync(IEnumerable<RequestBase> requests)
        {
            return (IEnumerable<ResultBase>)await RESTClient.PostAsync<IEnumerable<ResultBase>, IEnumerable<RequestBase>>(requests, null, default);
        }

        public async Task<IList<GetTokenBalanceResult>> GetTokenBalances(IList<GetTokenBalanceArgs> tokenBalanceArgsList)
        {
            if (tokenBalanceArgsList == null) throw new ArgumentNullException(nameof(tokenBalanceArgsList));

            var retVal = new List<GetTokenBalanceResult>();
            var argsById = new Dictionary<int, GetTokenBalanceArgs>();
            var requests = new List<RequestBase>();
            for (var i = 0; i < tokenBalanceArgsList.Count; i++)
            {
                var tokenBalanceArgs = tokenBalanceArgsList[i];
                var request = new RequestBase
                {
                    Id = i,
                    Method = Method.eth_call,
                    Params =
                        {
                            new TransactionCall
                            {
                                To = tokenBalanceArgs.Contract,
                                Data = $"0x70a08231000000000000000000000000{tokenBalanceArgs.Address.Substring(2,tokenBalanceArgs.Address.Length-2)}"
                            }, "latest"
                        }
                };
                argsById.Add(i, tokenBalanceArgs);
                requests.Add(request);
            }

            var results = await PostAsync(requests);

            foreach (var result in results)
            {
                //TODO: Long is again used and is no good
                var resultValue = result.Result.Length == 2 ? 0 : long.Parse(result.Result.Substring(2, result.Result.Length - 2), NumberStyles.HexNumber);

                var getTokenBalanceResult = new GetTokenBalanceResult
                {
                    Address = argsById[result.Id].Address,
                    Contract = argsById[result.Id].Contract,
                    Result = resultValue
                };
                retVal.Add(getTokenBalanceResult);
            }

            return retVal;
        }

        public async Task<IList<GetTokenBalanceResult>> GetTokenBalances(IEnumerable<string> addresses, IEnumerable<string> contracts)
        {
            var contractList = contracts.ToList();
            var tokenBalanceArgsList = (from address in addresses from contract in contractList select new GetTokenBalanceArgs { Address = address, Contract = contract }).ToList();

            return await GetTokenBalances(tokenBalanceArgsList);
        }

        public async Task<IEnumerable<TokenBalance>> GetTokenBalances(IEnumerable<string> addresses)
        {
            var retVal = new List<TokenBalance>();

            var tokenBalances = await GetTokenBalances(addresses, Tokens.Select(t => t.Contract));

            foreach (var tokenBalance in tokenBalances)
            {
                if (tokenBalance.Result == 0)
                {
                    continue;
                }

                var token = Tokens.First(t => t.Contract == tokenBalance.Contract);

                decimal balance = tokenBalance.Result;
                for (var i = 0; i < token.Decimals; i++)
                {
                    balance /= 10;
                }

                retVal.Add(new TokenBalance(new CurrencySymbol(token.Symbol), balance, tokenBalance.Address, token.Contract));
            }

            return retVal;
        }

        #endregion
    }
}
