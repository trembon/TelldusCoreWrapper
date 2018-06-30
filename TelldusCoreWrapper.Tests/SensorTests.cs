using System;
using System.Collections.Generic;
using System.Text;
using TelldusCoreWrapper.Entities;
using Xunit;

namespace TelldusCoreWrapper.Tests
{
    public class SensorTests
    {
        [Fact]
        public void GetAllSensorsWithValues()
        {
            using (ITelldusCoreService service = new TelldusCoreService())
            {
                service.Initialize();

                var sensors = service.GetSensors();
                Assert.NotEmpty(sensors);

                foreach (var sensor in sensors)
                {
                    TestSensor(sensor);

                    var values = service.GetSensorValues(sensor);
                    Assert.NotEmpty(values);

                    foreach (var value in values)
                        TestSensorValue(value);
                }
            }
        }

        private void TestSensor(Sensor sensor)
        {
            Assert.NotNull(sensor);

            Assert.InRange(sensor.ID, 1, int.MaxValue);
            
            Assert.NotNull(sensor.Model);
            Assert.NotNull(sensor.Protocol);

            Assert.True(sensor.Sensors > 0);
        }

        private void TestSensorValue(SensorValue sensorvalue)
        {
            Assert.NotNull(sensorvalue);

            Assert.InRange(sensorvalue.SensorID, 1, int.MaxValue);
            
            Assert.NotNull(sensorvalue.Value);

            Assert.True(sensorvalue.Type > 0);
            Assert.True(sensorvalue.Timestamp > DateTime.MinValue);
        }
    }
}
