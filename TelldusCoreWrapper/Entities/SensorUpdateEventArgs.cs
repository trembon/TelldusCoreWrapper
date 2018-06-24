using System;
using System.Collections.Generic;
using System.Text;
using TelldusCoreWrapper.Enums;

namespace TelldusCoreWrapper.Entities
{
    /// <summary>
    /// Event arguments for a sensor update event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class SensorUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the sensor value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public SensorValue Value { get; internal set; }
    }
}
