using System.Linq;
using System.Threading.Tasks;

namespace Hardwarewallets.Net.UnitTests
{
    public class LedgerDotNetApiTests : UnitTestBase
    {
        public override async Task Initialize()
        {
            var ledgerClient = (await LedgerClient.GetHIDLedgersAsync()).First();
            AddressDeriver = new LedgerDotNetApiWrapper(ledgerClient);
        }
    }
}
