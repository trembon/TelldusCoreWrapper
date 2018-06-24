using System;
using System.Collections.Generic;
using System.Text;

namespace TelldusCoreWrapper.Enums
{
    /// <summary>
    /// Supported value types on sensors from the Telldus service.
    /// </summary>
    [Flags]
    public enum SensorValueType : int
    {
        Temperature = 1,
        Humidity = 2,
        RainRate = 4,
        RainTotal = 8,
        WindDirection = 16,
        WindAverage = 32,
        WindGust = 64
    }
}
