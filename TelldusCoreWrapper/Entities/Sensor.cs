using System;
using System.Collections.Generic;
using System.Text;
using TelldusCoreWrapper.Enums;

namespace TelldusCoreWrapper.Entities
{
    /// <summary>
    /// Represents a sensor from the Telldus service.
    /// </summary>
    public class Sensor
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        /// <value>
        /// The protocol.
        /// </value>
        public string Protocol { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the sensors this device supports (Flags).
        /// </summary>
        /// <value>
        /// The sensors.
        /// </value>
        public SensorValueType Sensors { get; set; }
    }
}
