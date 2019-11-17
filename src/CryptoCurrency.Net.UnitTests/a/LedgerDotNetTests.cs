using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hardwarewallets.Net.UnitTests
{
    public class LedgerDotNetTests : UnitTestBase
    {
        public override async Task Initialize()
        {
            if (AddressDeriver != null)
            {
                return;
            }

            var ledgerDevice = await Connect();
            var ledgerManager = new LedgerManager(ledgerDevice);
            AddressDeriver = ledgerManager;
        }

        private async Task<IHidDevice> Connect()
        {
            DeviceInformation ledgerDeviceInformation = null;

            WindowsHidDevice retVal = null;

            retVal = new WindowsHidDevice();

            Console.Write("Waiting for Trezor .");

            while (ledgerDeviceInformation == null)
            {
                var devices = WindowsHidDevice.GetConnectedDeviceInformations();
                ledgerDeviceInformation = devices.FirstOrDefault(d => d.VendorId == 0x2c97);
                if (ledgerDeviceInformation != null)
                {
                    break;
                }

                await Task.Delay(1000);
                Console.Write(".");
            }

            retVal.DeviceInformation = ledgerDeviceInformation;

            await retVal.InitializeAsync();

            Console.WriteLine("Connected");

            return retVal;
        }
    }
}
