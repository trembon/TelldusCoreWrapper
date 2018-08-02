using System;
using System.Collections.Generic;
using System.Text;

namespace TelldusCoreWrapper
{
    /// <summary>
    /// A service to validate Telldus devices before adding them to the configuration.
    /// </summary>
    public interface ITelldusDeviceValidationService
    {
        /// <summary>
        /// Gets all the protocols.
        /// </summary>
        /// <returns>List of valid protocols.</returns>
        IEnumerable<string> GetProtocols();

        /// <summary>
        /// Gets all the models for all protocols.
        /// </summary>
        /// <returns>Distinct list of all valid models.</returns>
        IEnumerable<string> GetAllModels();

        /// <summary>
        /// Gets the models for a specific protocol.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <returns>List of valid models for the specified protocol.</returns>
        IEnumerable<string> GetModels(string protocol);

        /// <summary>
        /// Gets the parameters for a specific protocol/model combination.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="model">The model.</param>
        /// <returns>List of valid parameters for the specified protocol/model.</returns>
        IEnumerable<string> GetParameters(string protocol, string model);

        /// <summary>
        /// Determines whether this is a valid device with the specified arguments.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <returns>
        ///   <c>true</c> if this is a valid device based on the arguments; otherwise, <c>false</c>.
        /// </returns>
        bool IsValidDevice(string protocol);

        /// <summary>
        /// Determines whether this is a valid device with the specified arguments.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <c>true</c> if this is a valid device based on the arguments; otherwise, <c>false</c>.
        /// </returns>
        bool IsValidDevice(string protocol, string model);

        /// <summary>
        /// Determines whether this is a valid device with the specified arguments.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="model">The model.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this is a valid device based on the arguments; otherwise, <c>false</c>.
        /// </returns>
        bool IsValidDevice(string protocol, string model, string parameter);

        /// <summary>
        /// Determines whether this is a valid device with the specified arguments.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="model">The model.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <c>true</c> if this is a valid device based on the arguments; otherwise, <c>false</c>.
        /// </returns>
        bool IsValidDevice(string protocol, string model, IEnumerable<string> parameters);

        /// <summary>
        /// Determines whether this is a valid device with the specified arguments.
        /// This method will handle the parameters as name/value and validate the value with Regex if a pattern is available.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="model">The model.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <c>true</c> if this is a valid device based on the arguments; otherwise, <c>false</c>.
        /// </returns>
        bool IsValidDevice(string protocol, string model, IDictionary<string, string> parameters);
    }
}
