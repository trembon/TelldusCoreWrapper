using System;
using System.Collections.Generic;
using TelldusCoreWrapper.Entities;
using Xunit;

namespace TelldusCoreWrapper.Tests
{
    public class DeviceTests
    {
        [Fact]
        public void GetAllDevices()
        {
            using(ITelldusCoreService service = new TelldusCoreService())
            {
                service.Initialize();

                var devices = service.GetDevices();

                Assert.NotEmpty(devices);
                
                foreach(var device in devices)
                    TestDevice(device);
            }
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetDevice(int deviceId)
        {
            using (ITelldusCoreService service = new TelldusCoreService())
            {
                service.Initialize();

                Device device = service.GetDevice(deviceId);
                TestDevice(device);
            }
        }

        [Fact]
        public void AddAndRemoveDevice()
        {
            using (ITelldusCoreService service = new TelldusCoreService())
            {
                service.Initialize();

                string name = "Device from xUnit";
                string protocol = "arctech";
                string model = "selflearning-switch";
                Dictionary<string, string> parameters = new Dictionary<string, string> { { "house", "1" } };

                int deviceId = service.AddDevice(name, protocol, model, parameters);
                Assert.True(deviceId > 0);

                Device device = service.GetDevice(deviceId);
                TestDevice(device);

                bool result = service.RemoveDevice(deviceId);
                Assert.True(result);

                Device deviceAfterDeletion = service.GetDevice(deviceId);
                Assert.Null(deviceAfterDeletion);
            }
        }

        private void TestDevice(Device device)
        {
            Assert.NotNull(device);

            Assert.InRange(device.ID, 1, int.MaxValue);

            Assert.NotNull(device.Name);
            Assert.NotNull(device.Model);
            Assert.NotNull(device.Protocol);

            Assert.True(device.SupportedMethods > 0);
        }
    }
}
