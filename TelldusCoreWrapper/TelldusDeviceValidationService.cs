using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelldusCoreWrapper
{
    public class TelldusDeviceValidationService : ITelldusDeviceValidationService
    {
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

        private const string REGEX_A_TO_P = "[A-P]";
        private const string REGEX_1_TO_4 = "[1-4]";
        private const string REGEX_1_TO_15 = "[2-9]|1[0-5]?";
        private const string REGEX_1_TO_16 = "[2-9]|1[0-6]?";
        private const string REGEX_TRUE_FALSE = "true|false";

        //private static List<Device> devices = new List<Device>
        //{
        //    new Device("arctech", "bell", new string[]{ "house" })
        //};

        private static List<Protocol> validationData = new List<Protocol>();

        //private static string[] protocols = new string[]
        //{
        //    "arctech",
        //    "brateck",
        //    "everflourish",
        //    "fuhaote",
        //    "hasta",
        //    "ikea",
        //    "kangtai",
        //    "risingsun",
        //    "sartano",
        //    "silvanchip",
        //    "upm",
        //    "waveman",
        //    "x10",
        //    "yidong"
        //};

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

        public IEnumerable<string> GetProtocols()
        {
            return validationData
                .Select(p => p.Name);
        }

        public IEnumerable<string> GetAllModels()
        {
            return validationData
                .SelectMany(p => p.Models)
                .Select(m => m.Name);
        }

        public IEnumerable<string> GetModels(string protocol)
        {
            return validationData
                .FirstOrDefault(p => p.Name.Equals(protocol, StringComparison.OrdinalIgnoreCase))
                ?.Models
                .Select(m => m.Name);
        }

        public IEnumerable<string> GetParameters(string protocol, string model)
        {
            return validationData
                .FirstOrDefault(p => p.Name.Equals(protocol, StringComparison.OrdinalIgnoreCase))
                ?.Models
                .FirstOrDefault(m => m.Name.Equals(model, StringComparison.OrdinalIgnoreCase))
                ?.Parameters
                .Select(p => p.Name);
        }

        public bool IsValidDevice(string protocol)
        {
            throw new NotImplementedException();
        }

        public bool IsValidDevice(string protocol, string model)
        {
            throw new NotImplementedException();
        }

        public bool IsValidDevice(string protocol, string model, string parameter)
        {
            throw new NotImplementedException();
        }

        public bool IsValidDevice(string protocol, string model, IEnumerable<string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
