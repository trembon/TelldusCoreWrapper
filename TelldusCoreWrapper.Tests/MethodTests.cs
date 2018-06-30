using System;
using System.Collections.Generic;
using System.Text;
using TelldusCoreWrapper.Enums;
using Xunit;

namespace TelldusCoreWrapper.Tests
{
    public class MethodTests
    {
        [Theory]
        [InlineData(1, DeviceMethods.TurnOn)]
        [InlineData(1, DeviceMethods.TurnOff)]
        [InlineData(2, DeviceMethods.Learn)]
        public void SendCommand(int deviceId, DeviceMethods command)
        {
            using (ITelldusCoreService service = new TelldusCoreService())
            {
                service.Initialize();

                ResultCode result = service.SendCommand(deviceId, command);
                Assert.Equal(ResultCode.Success, result);
            }
        }

        [Fact]
        public void TurnOn()
        {
            using (ITelldusCoreService service = new TelldusCoreService())
            {
                service.Initialize();

                ResultCode result = service.TurnOn(1);
                Assert.Equal(ResultCode.Success, result);
            }
        }

        [Fact]
        public void TurnOff()
        {
            using (ITelldusCoreService service = new TelldusCoreService())
            {
                service.Initialize();

                ResultCode result = service.TurnOff(1);
                Assert.Equal(ResultCode.Success, result);
            }
        }

        [Fact]
        public void Learn()
        {
            using (ITelldusCoreService service = new TelldusCoreService())
            {
                service.Initialize();

                ResultCode result = service.Learn(2);
                Assert.Equal(ResultCode.Success, result);
            }
        }
    }
}
