using System;
using System.Collections.Generic;
using System.Text;
using TelldusCoreWrapper.Enums;

namespace TelldusCoreWrapper.Entities
{
    /// <summary>
    /// Represents a device from the Telldus service.
    /// </summary>
    public sealed class Device
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; internal set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public string Model { get; internal set; }

        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        /// <value>
        /// The protocol.
        /// </value>
        public string Protocol { get; internal set; }

        /// <summary>
        /// Gets or sets the supported methods (Flags).
        /// </summary>
        /// <value>
        /// The supported methods.
        /// </value>
        public DeviceMethods SupportedMethods { get; internal set; }

        internal Device()
        {

        }
    }
}
