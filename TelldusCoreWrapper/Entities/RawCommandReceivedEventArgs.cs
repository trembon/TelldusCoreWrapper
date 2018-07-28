using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelldusCoreWrapper.Entities
{
    /// <summary>
    /// Event arguments for receiving a raw command.
    /// </summary>
    public sealed class RawCommandReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// On which controller the command was recieved on.
        /// </summary>
        public int ControllerID { get; }

        /// <summary>
        /// Parsed values from the raw data property.
        /// </summary>
        public Dictionary<string, string> Values { get; }

        /// <summary>
        /// The raw command that was received.
        /// </summary>
        public string RawData { get; }

        internal RawCommandReceivedEventArgs(int controllerId, string rawData)
        {
            this.ControllerID = controllerId;
            this.RawData = rawData;

            this.Values = rawData
                .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries))
                .Where(line => line.Length == 2)
                .ToDictionary(k => k[0], v => v[1]);
        }
    }
}
