using CryptoCurrency.Net.APIClients;
using CryptoCurrency.Net.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoCurrency.Net.UnitTests
{
    [TestClass]
    public class ApiSpecificTests
    {
        [TestMethod]
        public async Task TestEtherscanGetAddress()
        {
            var asdasd = new EtherscanClient(CurrencySymbol.Ethereum, new RESTClientFactory());
            var asdassd = await asdasd.GetAddresses(new List<string>
                {
                    "0x0E95F8F8ecBd770585766c1CD216C81aA43439a6",
                    "0xda4D788FA55CDE88C8dc93Ceb4Ce9EDCf26Ee2A5",
                    "0x26769f254f1Ba073cEF6e9E47a7320332a4dA3D8"
                });
        }
    }
}
