using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace TelldusCoreWrapper.Tests
{
    public class ValidationTests
    {
        [Fact]
        public void GetProtocols_NotEmpty()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            IEnumerable<string> protocols = service.GetProtocols();

            Assert.NotEmpty(protocols);
        }

        [Fact]
        public void GetProtocols_NotEmptyValues()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            IEnumerable<string> protocols = service.GetProtocols();
            
            Assert.All(protocols, s => {
                Assert.NotNull(s);
                Assert.NotEqual<string>("", s);
            });
        }

        [Fact]
        public void GetProtocols_Count()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            IEnumerable<string> protocols = service.GetProtocols();
            
            Assert.True(protocols.Count() == 13);
        }

        [Fact]
        public void GetAllModels_NotEmpty()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            IEnumerable<string> models = service.GetAllModels();

            Assert.NotEmpty(models);
        }

        [Fact]
        public void GetAllModels_NotEmptyValues()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            IEnumerable<string> models = service.GetAllModels();

            Assert.All(models, s => {
                Assert.NotNull(s);
                Assert.NotEqual<string>("", s);
            });
        }

        [Fact]
        public void GetAllModels_Count()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            IEnumerable<string> models = service.GetAllModels();
            
            Assert.True(models.Count() == 7);
        }

        [Fact]
        public void GetModels_NoEmptyResults()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            IEnumerable<string> protocols = service.GetProtocols();

            foreach(string protocol in protocols)
            {
                IEnumerable<string> models = service.GetModels(protocol);

                Assert.NotEmpty(models);
                Assert.All(models, s => {
                    Assert.NotNull(s);
                    Assert.NotEqual<string>("", s);
                });
            }
        }

        [Fact]
        public void GetModels_Count_archtech()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            IEnumerable<string> models = service.GetModels("arctech");

            Assert.True(models.Count() == 4);
        }

        [Fact]
        public void GetModels_Count_everflourish()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            IEnumerable<string> models = service.GetModels("everflourish");

            Assert.True(models.Count() == 2);
        }

        [Fact]
        public void GetModels_Count_sartano()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            IEnumerable<string> models = service.GetModels("sartano");

            Assert.True(models.Count() == 1);
        }

        [Fact]
        public void GetParameters_Count_archtech_codeswitch()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            IEnumerable<string> parameters = service.GetParameters("arctech", "codeswitch");

            Assert.True(parameters.Count() == 2);
        }

        [Fact]
        public void GetParameters_Count_archtech_bell()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            IEnumerable<string> parameters = service.GetParameters("arctech", "bell");

            Assert.True(parameters.Count() == 1);
        }

        [Fact]
        public void GetParameters_Count_risingsun_codeswitch()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            IEnumerable<string> parameters = service.GetParameters("risingsun", "codeswitch");

            Assert.True(parameters.Count() == 2);
        }
        
        [Fact]
        public void IsValidDevice_ProtocolExists()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            Assert.True(service.IsValidDevice("risingsun"));
        }

        [Fact]
        public void IsValidDevice_ProtocolNotExists()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            Assert.False(service.IsValidDevice("asd"));
        }

        [Fact]
        public void IsValidDevice_ProtocolNull()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            Assert.False(service.IsValidDevice(null));
        }

        [Fact]
        public void IsValidDevice_ModelExists()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            Assert.True(service.IsValidDevice("arctech", "codeswitch"));
        }

        [Fact]
        public void IsValidDevice_ModelNotExists()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            Assert.False(service.IsValidDevice("arctech", "asd"));
        }

        [Fact]
        public void IsValidDevice_ModelNull()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            Assert.False(service.IsValidDevice("arctech", null));
        }

        [Fact]
        public void IsValidDevice_ParameterExists()
        {
            // TODO: should device that does not contain all parameters be valid?
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            Assert.True(service.IsValidDevice("arctech", "codeswitch", "house"));
        }

        [Fact]
        public void IsValidDevice_ParametersExists()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            Assert.True(service.IsValidDevice("arctech", "codeswitch", new string[] { "house", "unit" }));
        }

        [Fact]
        public void IsValidDevice_ParameterNotExists()
        {
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            Assert.False(service.IsValidDevice("arctech", "codeswitch", "asd"));
        }

        [Fact]
        public void IsValidDevice_ParameterEmpty()
        {
            // TODO: should device that does not contain all parameters be valid?
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            Assert.True(service.IsValidDevice("arctech", "codeswitch", new string[0]));
        }

        [Fact]
        public void IsValidDevice_ParameterWithValues_Valid()
        {
            // TODO: should device that does not contain all parameters be valid?
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            Assert.True(service.IsValidDevice("arctech", "codeswitch", new Dictionary<string, string> { { "house", "B" }, { "unit", "5" } }));
        }

        [Fact]
        public void IsValidDevice_ParameterWithValues_Invalid()
        {
            // TODO: should device that does not contain all parameters be valid?
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            Assert.False(service.IsValidDevice("arctech", "codeswitch", new Dictionary<string, string> { { "house", "T" }, { "unit", "Y" } }));
        }

        [Fact]
        public void IsValidDevice_ParameterWithValues_NoValidation()
        {
            // TODO: should device that does not contain all parameters be valid?
            ITelldusDeviceValidationService service = new TelldusDeviceValidationService();
            Assert.True(service.IsValidDevice("brateck", "codeswitch", new Dictionary<string, string> { { "house", "HELLO WORLD" } }));
        }
    }
}
