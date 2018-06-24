using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TelldusCoreWrapper.Wrappers
{
    internal static class UnixWrapper
    {
        private const string LIB_TELLDUS_CORE_SO = "libtelldus-core.so";

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern void tdInit();

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern int tdRegisterSensorEvent(NativeWrapper.TDSensorEvent eventFunction, IntPtr context);

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern void tdClose();

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern void tdReleaseString(IntPtr thestring);


        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern int tdTurnOn(int intDeviceId);

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern int tdTurnOff(int intDeviceId);

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern int tdLearn(int intDeviceId);

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern int tdMethods(int id, int methodsSupported);

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern int tdLastSentCommand(int intDeviceId, int methodsSupported);


        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern int tdGetNumberOfDevices();

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern int tdGetDeviceId(int intDeviceIndex);

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern int tdGetDeviceType(int intDeviceId);


        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern IntPtr tdGetName(int intDeviceId);

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern bool tdSetName(int intDeviceId, IntPtr chNewName);

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern IntPtr tdGetProtocol(int intDeviceId);

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern bool tdSetProtocol(int intDeviceId, IntPtr strProtocol);

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern IntPtr tdGetModel(int intDeviceId);

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern bool tdSetModel(int intDeviceId, IntPtr intModel);


        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern IntPtr tdGetDeviceParameter(int intDeviceId, IntPtr strName, IntPtr defaultValue);

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern bool tdSetDeviceParameter(int intDeviceId, IntPtr strName, IntPtr strValue);


        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern int tdAddDevice();

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern bool tdRemoveDevice(int intDeviceId);


        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern int tdSensor(IntPtr protocol, int protocolLength, IntPtr model, int modelLength, IntPtr id, IntPtr dataTypes);

        [DllImport(LIB_TELLDUS_CORE_SO)]
        public static extern int tdSensorValue(IntPtr protocol, IntPtr model, int id, int dataType, IntPtr value, int valueLength, IntPtr timestamp);
    }
}
