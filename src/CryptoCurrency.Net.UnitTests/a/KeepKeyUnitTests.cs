using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hardwarewallets.Net.UnitTests
{
    public class KeepKeyUnitTests : UnitTestBase
    {
        public override async Task Initialize()
        {
            if (AddressDeriver != null)
            {
                return;
            }

            var keepKeyDevice = await Connect();
            var keepKeyManager = new KeepKeyManager(GetPin, keepKeyDevice);
            await keepKeyManager.InitializeAsync();
            AddressDeriver = keepKeyManager;
        }

        private async Task<IHidDevice> Connect()
        {
            DeviceInformation keepKeyDeviceInformation = null;

            WindowsHidDevice retVal = null;

            retVal = new WindowsHidDevice();

            Console.Write("Waiting for Trezor .");

            while (keepKeyDeviceInformation == null)
            {
                var devices = WindowsHidDevice.GetConnectedDeviceInformations();
                keepKeyDeviceInformation = devices.FirstOrDefault(d => d.VendorId == KeepKeyManager.VendorId && KeepKeyManager.ProductId == d.ProductId);

                if (keepKeyDeviceInformation != null)
                {
                    break;
                }

                await Task.Delay(1000);
                Console.Write(".");
            }

            retVal.DeviceInformation = keepKeyDeviceInformation;
            retVal.DataHasExtraByte = false;
            await retVal.InitializeAsync();

            Console.WriteLine("Connected");

            return retVal;
        }

        private async Task<string> GetPin()
        {
            throw new Exception("The pin needs to be initialized before running the unit test");
        }
    }
}
