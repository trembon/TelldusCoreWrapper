using System;
using System.Collections.Generic;
using System.Text;

namespace TelldusCoreWrapper
{
    public interface ITelldusDeviceValidationService
    {
        IEnumerable<string> GetProtocols();

        IEnumerable<string> GetAllModels();

        IEnumerable<string> GetModels(string protocol);

        IEnumerable<string> GetParameters(string protocol, string model);

        bool IsValidDevice(string protocol);

        bool IsValidDevice(string protocol, string model);

        bool IsValidDevice(string protocol, string model, string parameter);

        bool IsValidDevice(string protocol, string model, IEnumerable<string> parameters);
    }
}
