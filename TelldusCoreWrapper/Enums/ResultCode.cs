using System;
using System.Collections.Generic;
using System.Text;

namespace TelldusCoreWrapper.Enums
{
    /// <summary>
    /// Possible codes returned from the Telldus service.
    /// </summary>
    public enum ResultCode : int
    {
        Success = 0,
        NotFound = -1,
        PerissionDenied = -2,
        DeviceNotFound = -3,
        MethodNotSupported = -4,
        CommunicationError = -5,
        ErrorConnectingService = -6,
        UnknownResponse = -7,
        SyntaxError = -8,
        BrokenPipe = -9,
        ErrorCommunicatingService = -10,
        ConfigSyntaxError = -11,
        UnknownError = -99
    }
}
