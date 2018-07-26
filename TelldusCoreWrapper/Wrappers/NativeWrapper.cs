using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TelldusCoreWrapper.Wrappers
{
    internal static class NativeWrapper
    {
        public delegate void TDDeviceEvent(int deviceId, int method, IntPtr data, int callbackId, IntPtr context);
        public delegate void TDDeviceChangeEvent(int deviceId, int changeEvent, int changeType, int callbackId, IntPtr context);
        public delegate void TDRawDeviceEvent(IntPtr data, int controllerId, int callbackId, IntPtr context);
        public delegate void TDSensorEvent(IntPtr protocol, IntPtr model, int id, int dataType, IntPtr value, int timestamp, int callbackId, IntPtr context);
        public delegate void TDControllerEvent(int controllerId, int changeEvent, int changeType, IntPtr newValue, int callbackId, IntPtr context);

        private static bool isWindows = false;

        static NativeWrapper()
        {
            isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        public static void tdInit()
        {
            if (isWindows)
            {
                WindowsWrapper.tdInit();
            }
            else
            {
                UnixWrapper.tdInit();
            }
        }

        public static int tdRegisterDeviceEvent(TDDeviceEvent eventFunction, IntPtr context)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdRegisterDeviceEvent(eventFunction, context);
            }
            else
            {
                return UnixWrapper.tdRegisterDeviceEvent(eventFunction, context);
            }
        }

        public static int tdRegisterDeviceChangeEvent(TDDeviceChangeEvent eventFunction, IntPtr context)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdRegisterDeviceChangeEvent(eventFunction, context);
            }
            else
            {
                return UnixWrapper.tdRegisterDeviceChangeEvent(eventFunction, context);
            }
        }

        public static int tdRegisterRawDeviceEvent(TDRawDeviceEvent eventFunction, IntPtr context)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdRegisterRawDeviceEvent(eventFunction, context);
            }
            else
            {
                return UnixWrapper.tdRegisterRawDeviceEvent(eventFunction, context);
            }
        }

        public static int tdRegisterSensorEvent(TDSensorEvent eventFunction, IntPtr context)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdRegisterSensorEvent(eventFunction, context);
            }
            else
            {
                return UnixWrapper.tdRegisterSensorEvent(eventFunction, context);
            }
        }

        public static int tdRegisterControllerEvent(TDControllerEvent eventFunction, IntPtr context)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdRegisterControllerEvent(eventFunction, context);
            }
            else
            {
                return UnixWrapper.tdRegisterControllerEvent(eventFunction, context);
            }
        }

        public static void tdClose()
        {
            if (isWindows)
            {
                WindowsWrapper.tdClose();
            }
            else
            {
                UnixWrapper.tdClose();
            }
        }

        public static void tdReleaseString(IntPtr thestring)
        {
            if (isWindows)
            {
                WindowsWrapper.tdReleaseString(thestring);
            }
            else
            {
                UnixWrapper.tdReleaseString(thestring);
            }
        }


        public static int tdTurnOn(int intDeviceId)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdTurnOn(intDeviceId);
            }
            else
            {
                return UnixWrapper.tdTurnOn(intDeviceId);
            }
        }

        public static int tdTurnOff(int intDeviceId)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdTurnOff(intDeviceId);
            }
            else
            {
                return UnixWrapper.tdTurnOff(intDeviceId);
            }
        }

        public static int tdLearn(int intDeviceId)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdLearn(intDeviceId);
            }
            else
            {
                return UnixWrapper.tdLearn(intDeviceId);
            }
        }

        public static int tdMethods(int id, int methodsSupported)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdMethods(id, methodsSupported);
            }
            else
            {
                return UnixWrapper.tdMethods(id, methodsSupported);
            }
        }

        public static int tdLastSentCommand(int intDeviceId, int methodsSupported)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdLastSentCommand(intDeviceId, methodsSupported);
            }
            else
            {
                return UnixWrapper.tdLastSentCommand(intDeviceId, methodsSupported);
            }
        }


        public static int tdGetNumberOfDevices()
        {
            if (isWindows)
            {
                return WindowsWrapper.tdGetNumberOfDevices();
            }
            else
            {
                return UnixWrapper.tdGetNumberOfDevices();
            }
        }

        public static int tdGetDeviceId(int deviceIndex)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdGetDeviceId(deviceIndex);
            }
            else
            {
                return UnixWrapper.tdGetDeviceId(deviceIndex);
            }
        }

        public static int tdGetDeviceType(int deviceId)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdGetDeviceType(deviceId);
            }
            else
            {
                return UnixWrapper.tdGetDeviceType(deviceId);
            }
        }


        public static IntPtr tdGetName(int intDeviceId)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdGetName(intDeviceId);
            }
            else
            {
                return UnixWrapper.tdGetName(intDeviceId);
            }
        }
        
        public static bool tdSetName(int intDeviceId, IntPtr chNewName)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdSetName(intDeviceId, chNewName);
            }
            else
            {
                return UnixWrapper.tdSetName(intDeviceId, chNewName);
            }
        }

        public static IntPtr tdGetProtocol(int intDeviceId)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdGetProtocol(intDeviceId);
            }
            else
            {
                return UnixWrapper.tdGetProtocol(intDeviceId);
            }
        }

        public static bool tdSetProtocol(int intDeviceId, IntPtr strProtocol)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdSetProtocol(intDeviceId, strProtocol);
            }
            else
            {
                return UnixWrapper.tdSetProtocol(intDeviceId, strProtocol);
            }
        }

        public static IntPtr tdGetModel(int intDeviceId)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdGetModel(intDeviceId);
            }
            else
            {
                return UnixWrapper.tdGetModel(intDeviceId);
            }
        }

        public static bool tdSetModel(int intDeviceId, IntPtr intModel)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdSetModel(intDeviceId, intModel);
            }
            else
            {
                return UnixWrapper.tdSetModel(intDeviceId, intModel);
            }
        }

        
        public static IntPtr tdGetDeviceParameter(int intDeviceId, IntPtr strName, IntPtr defaultValue)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdGetDeviceParameter(intDeviceId, strName, defaultValue);
            }
            else
            {
                return UnixWrapper.tdGetDeviceParameter(intDeviceId, strName, defaultValue);
            }
        }

        public static bool tdSetDeviceParameter(int intDeviceId, IntPtr strName, IntPtr strValue)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdSetDeviceParameter(intDeviceId, strName, strValue);
            }
            else
            {
                return UnixWrapper.tdSetDeviceParameter(intDeviceId, strName, strValue);
            }
        }



        public static int tdAddDevice()
        {
            if (isWindows)
            {
                return WindowsWrapper.tdAddDevice();
            }
            else
            {
                return UnixWrapper.tdAddDevice();
            }
        }
        
        public static bool tdRemoveDevice(int intDeviceId)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdRemoveDevice(intDeviceId);
            }
            else
            {
                return UnixWrapper.tdRemoveDevice(intDeviceId);
            }
        }


        public static int tdSensor(IntPtr protocol, int protocolLength, IntPtr model, int modelLength, IntPtr id, IntPtr dataTypes)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdSensor(protocol, protocolLength, model, modelLength, id, dataTypes);
            }
            else
            {
                return UnixWrapper.tdSensor(protocol, protocolLength, model, modelLength, id, dataTypes);
            }
        }

        public static int tdSensorValue(IntPtr protocol, IntPtr model, int id, int dataType, IntPtr value, int valueLength, IntPtr timestamp)
        {
            if (isWindows)
            {
                return WindowsWrapper.tdSensorValue(protocol, model, id, dataType, value, valueLength, timestamp);
            }
            else
            {
                return UnixWrapper.tdSensorValue(protocol, model, id, dataType, value, valueLength, timestamp);
            }
        }
    }
}
