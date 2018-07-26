using System;
using System.Collections.Generic;
using System.Text;
using TelldusCoreWrapper.Enums;

namespace TelldusCoreWrapper.Entities
{
    /// <summary>
    /// Event arguments for a command sent event.
    /// </summary>
    public sealed class CommandSentEventArgs : EventArgs
    {
        /// <summary>
        /// The device that the command was sent to.
        /// </summary>
        public Device Device { get; }

        /// <summary>
        /// The command that was sent.
        /// </summary>
        public DeviceMethods Command { get; }

        /// <summary>
        /// The parameter for the command, if any.
        /// </summary>
        public string Parameter { get; }

        internal CommandSentEventArgs(Device device, DeviceMethods command, string parameter)
        {
            this.Device = device;
            this.Command = command;
            this.Parameter = parameter;
        }
    }
}
