using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hardwarewallets.Net.UnitTests
{
    public class TrezorUnitTests : UnitTestBase
    {
        public override async Task Initialize()
        {
            if (AddressDeriver != null)
            {
                return;
            }

            var trezorHidDevice = await Connect();
            var trezorManager = new TrezorManager(GetPin, trezorHidDevice);
            await trezorManager.InitializeAsync();
            AddressDeriver = trezorManager;
        }

        private async Task<IHidDevice> Connect()
        {
            DeviceInformation trezorDeviceInformation = null;

            WindowsHidDevice retVal = null;

            retVal = new WindowsHidDevice();

            Console.Write("Waiting for Trezor .");

            while (trezorDeviceInformation == null)
            {
                var devices = WindowsHidDevice.GetConnectedDeviceInformations();
                var trezors = devices.Where(d => d.VendorId == TrezorManager.TrezorVendorId && TrezorManager.TrezorProductId == 1).ToList();
                trezorDeviceInformation = trezors.FirstOrDefault(t => t.Product == TrezorManager.USBOneName);

                if (trezorDeviceInformation != null)
                {
                    break;
                }

                await Task.Delay(1000);
                Console.Write(".");
            }

            retVal.DeviceInformation = trezorDeviceInformation;

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
