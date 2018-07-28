using System;
using System.Collections.Generic;
using System.Text;
using TelldusCoreWrapper.Enums;

namespace TelldusCoreWrapper.Entities
{
    /// <summary>
    /// Event arguments for receiving a command.
    /// </summary>
    public sealed class CommandReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// The device that the command was received for.
        /// </summary>
        public Device Device { get; }

        /// <summary>
        /// The command that was received.
        /// </summary>
        public DeviceMethods Command { get; }

        /// <summary>
        /// The parameter for the command, if any.
        /// </summary>
        public string Parameter { get; }

        internal CommandReceivedEventArgs(Device device, DeviceMethods command, string parameter)
        {
            this.Device = device;
            this.Command = command;
            this.Parameter = parameter;
        }
    }
}
