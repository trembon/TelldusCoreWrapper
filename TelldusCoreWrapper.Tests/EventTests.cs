using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace TelldusCoreWrapper.Tests
{
    public class EventTests
    {
        private const int WAIT_TIME = 30 * 1000; // 30 seconds

        [Fact]
        public void CommandReceived_ReceivesEvent()
        {
            using (ITelldusCoreService service = new TelldusCoreService())
            {
                service.Initialize();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                service.CommandReceived += (s, eventArgs) =>
                {
                    Assert.NotNull(eventArgs.Device);

                    resetEvent.Set();
                };

                bool result = resetEvent.WaitOne(WAIT_TIME);
                
                Assert.True(result);
            }
        }

        [Fact]
        public void RawCommandReceived_ReceivesEvent()
        {
            using (ITelldusCoreService service = new TelldusCoreService())
            {
                service.Initialize();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                service.RawCommandReceived += (s, eventArgs) =>
                {
                    Assert.NotNull(eventArgs.RawData);
                    Assert.NotNull(eventArgs.Values);

                    resetEvent.Set();
                };

                bool result = resetEvent.WaitOne(WAIT_TIME);

                Assert.True(result);
            }
        }

        [Fact]
        public void SensorUpdated_ReceivesEvent()
        {
            using (ITelldusCoreService service = new TelldusCoreService())
            {
                service.Initialize();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                service.SensorUpdated += (s, eventArgs) =>
                {
                    Assert.NotNull(eventArgs.Value);
                    
                    resetEvent.Set();
                };

                bool result = resetEvent.WaitOne(WAIT_TIME);

                Assert.True(result);
            }
        }
    }
}
