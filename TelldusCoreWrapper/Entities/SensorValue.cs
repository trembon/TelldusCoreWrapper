using System;
using System.Collections.Generic;
using System.Text;
using TelldusCoreWrapper.Enums;

namespace TelldusCoreWrapper.Entities
{
    /// <summary>
    /// Represents a value for a specific sensor type from the Telldus service.
    /// </summary>
    public sealed class SensorValue
    {
        /// <summary>
        /// Gets or sets the sensor identifier.
        /// </summary>
        /// <value>
        /// The sensor identifier.
        /// </value>
        public int SensorID { get; set; }

        /// <summary>
        /// Gets or sets the value type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public SensorValueType Type { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the updated occured.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        internal SensorValue()
        {

        }
    }
}
