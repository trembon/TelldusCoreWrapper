using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TelldusCoreWrapper
{
    /// <summary>
    /// A service to validate Telldus devices before adding them to the configuration.
    /// </summary>
    /// <seealso cref="TelldusCoreWrapper.ITelldusDeviceValidationService" />
    public class TelldusDeviceValidationService : ITelldusDeviceValidationService
    {
        #region Collection classes
        private class Protocol
        {
            public string Name { get; set; }

            public List<Model> Models { get; set; }

            public Protocol(string name)
            {
                this.Name = name;
                this.Models = new List<Model>();
            }

            public Model AddModel(string name)
            {
                Model model = new Model(name);
                Models.Add(model);
                return model;
            }
        }

        private class Model
        {
            public string Name { get; set; }

            public List<Parameter> Parameters { get; set; }

            public Model(string name)
            {
                this.Name = name;
                this.Parameters = new List<Parameter>();
            }

            public Model AddParameter(string name)
            {
                return AddParameter(name, null);
            }

            public Model AddParameter(string name, string validationRegex)
            {
                this.Parameters.Add(new Parameter { Name = name, ValidationRegex = validationRegex });
                return this;
            }
        }

        private class Parameter
        {
            public string Name { get; set; }

            public string ValidationRegex { get; set; }
        }
        #endregion

        private const string REGEX_A_TO_P = "[A-P]";
        private const string REGEX_1_TO_4 = "[1-4]";
        private const string REGEX_1_TO_15 = "[2-9]|1[0-5]?";
        private const string REGEX_1_TO_16 = "[2-9]|1[0-6]?";
        private const string REGEX_TRUE_FALSE = "true|false";

        private static List<Protocol> validationData = new List<Protocol>();

        #region Initialization
        static TelldusDeviceValidationService()
        {
            Initialize();
        }

        private static void Initialize()
        {
            AddProtocol("arctech", p =>
            {
                p.AddModel("codeswitch").AddParameter("house", REGEX_A_TO_P).AddParameter("unit", REGEX_1_TO_16);
                p.AddModel("bell").AddParameter("house", REGEX_A_TO_P);
                p.AddModel("selflearning-switch").AddParameter("house").AddParameter("unit", REGEX_1_TO_16);
                p.AddModel("selflearning-dimmer").AddParameter("house").AddParameter("unit", REGEX_1_TO_16);
            });

            AddProtocol("brateck", p =>
            {
                p.AddModel("codeswitch").AddParameter("house");
            });

            AddProtocol("everflourish", p =>
            {
                p.AddModel("codeswitch").AddParameter("house").AddParameter("unit");
                p.AddModel("selflearning").AddParameter("house").AddParameter("unit");
            });

            AddProtocol("fuhaote", p =>
            {
                p.AddModel("codeswitch").AddParameter("code");
            });

            AddProtocol("hasta", p =>
            {
                p.AddModel("selflearning").AddParameter("house").AddParameter("unit", REGEX_1_TO_15);
            });

            AddProtocol("ikea", p =>
            {
                p.AddModel("selflearning-switch").AddParameter("system", REGEX_1_TO_16).AddParameter("units");
                p.AddModel("selflearning").AddParameter("system", REGEX_1_TO_16).AddParameter("units").AddParameter("fade", REGEX_TRUE_FALSE);
            });

            // TODO: keep this? only tellstick net support
            //AddProtocol("kangtai", p =>
            //{
            //    p.AddModel("selflearning");
            //});

            AddProtocol("risingsun", p =>
            {
                p.AddModel("codeswitch").AddParameter("house", REGEX_1_TO_4).AddParameter("unit", REGEX_1_TO_4);
                p.AddModel("selflearning").AddParameter("house").AddParameter("unit", REGEX_1_TO_16);
            });

            AddProtocol("sartano", p =>
            {
                p.AddModel("codeswitch").AddParameter("code", REGEX_1_TO_4);
            });

            AddProtocol("silvanchip", p =>
            {
                p.AddModel("ecosavers").AddParameter("house").AddParameter("unit", REGEX_1_TO_4);
                p.AddModel("kp100").AddParameter("house");
            });

            AddProtocol("upm", p =>
            {
                p.AddModel("selflearning").AddParameter("house").AddParameter("unit", REGEX_1_TO_4);
            });

            AddProtocol("waveman", p =>
            {
                p.AddModel("codeswitch").AddParameter("house", REGEX_A_TO_P).AddParameter("unit", REGEX_1_TO_16);
            });

            AddProtocol("x10", p =>
            {
                p.AddModel("codeswitch").AddParameter("house", REGEX_A_TO_P).AddParameter("unit", REGEX_1_TO_16);
            });

            AddProtocol("yidong", p =>
            {
                p.AddModel("codeswitch").AddParameter("unit", REGEX_1_TO_4);
            });
        }

        private static void AddProtocol(string name, Action<Protocol> configure)
        {
            Protocol protocol = new Protocol(name);
            configure(protocol);
            validationData.Add(protocol);
        }
        #endregion

        #region Get methods
        /// <summary>
        /// Gets all the protocols.
        /// </summary>
        /// <returns>
        /// List of valid protocols.
        /// </returns>
        public IEnumerable<string> GetProtocols()
        {
            return validationData
                .Select(p => p.Name);
        }

        /// <summary>
        /// Gets all the models for all protocols.
        /// </summary>
        /// <returns>
        /// Distinct list of all valid models.
        /// </returns>
        public IEnumerable<string> GetAllModels()
        {
            return validationData
                .SelectMany(p => p.Models)
                .Select(m => m.Name)
                .Distinct();
        }

        /// <summary>
        /// Gets the models for a specific protocol.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <returns>
        /// List of valid models for the specified protocol.
        /// </returns>
        public IEnumerable<string> GetModels(string protocol)
        {
            return validationData
                .FirstOrDefault(p => p.Name.Equals(protocol, StringComparison.Ordinal))
                ?.Models
                .Select(m => m.Name);
        }

        /// <summary>
        /// Gets the parameters for a specific protocol/model combination.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="model">The model.</param>
        /// <returns>
        /// List of valid parameters for the specified protocol/model.
        /// </returns>
        public IEnumerable<string> GetParameters(string protocol, string model)
        {
            return validationData
                .FirstOrDefault(p => p.Name.Equals(protocol, StringComparison.Ordinal))
                ?.Models
                .FirstOrDefault(m => m.Name.Equals(model, StringComparison.Ordinal))
                ?.Parameters
                .Select(p => p.Name);
        }
        #endregion

        #region Validation methods
        /// <summary>
        /// Determines whether this is a valid device with the specified arguments.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <returns>
        ///   <c>true</c> if this is a valid device based on the arguments; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidDevice(string protocol)
        {
            return validationData
                .Any(p => p.Name.Equals(protocol, StringComparison.Ordinal));
        }

        /// <summary>
        /// Determines whether this is a valid device with the specified arguments.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <c>true</c> if this is a valid device based on the arguments; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidDevice(string protocol, string model)
        {
            return validationData
                .Where(p => p.Name.Equals(protocol, StringComparison.Ordinal))
                .SelectMany(p => p.Models)
                .Any(m => m.Name.Equals(model, StringComparison.Ordinal));
        }

        /// <summary>
        /// Determines whether this is a valid device with the specified arguments.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="model">The model.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this is a valid device based on the arguments; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidDevice(string protocol, string model, string parameter)
        {
            return validationData
                .Where(p => p.Name.Equals(protocol, StringComparison.Ordinal))
                .SelectMany(p => p.Models)
                .Where(m => m.Name.Equals(model, StringComparison.Ordinal))
                .SelectMany(m => m.Parameters)
                .Any(p => p.Name.Equals(parameter, StringComparison.Ordinal));
        }

        /// <summary>
        /// Determines whether this is a valid device with the specified arguments.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="model">The model.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <c>true</c> if this is a valid device based on the arguments; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidDevice(string protocol, string model, IEnumerable<string> parameters)
        {
            IEnumerable<string> validParameters = validationData
                .Where(p => p.Name.Equals(protocol, StringComparison.Ordinal))
                .SelectMany(p => p.Models)
                .Where(m => m.Name.Equals(model, StringComparison.Ordinal))
                .SelectMany(m => m.Parameters)
                .Select(p => p.Name);

            foreach (string parameter in parameters)
                if (!validParameters.Contains(parameter))
                    return false;

            return true;
        }

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
        public bool IsValidDevice(string protocol, string model, IDictionary<string, string> parameters)
        {
            IEnumerable<Parameter> validParameters = validationData
                .Where(p => p.Name.Equals(protocol, StringComparison.Ordinal))
                .SelectMany(p => p.Models)
                .Where(m => m.Name.Equals(model, StringComparison.Ordinal))
                .SelectMany(m => m.Parameters);

            foreach (var parameter in parameters)
            {
                Parameter validParameter = validParameters.FirstOrDefault(p => p.Name.Equals(parameter.Key, StringComparison.Ordinal));
                if (validParameter == null)
                    return false;

                if (validParameter.ValidationRegex != null && !Regex.IsMatch(parameter.Value, validParameter.ValidationRegex))
                    return false;
            }

            return true;
        }
        #endregion
    }
}