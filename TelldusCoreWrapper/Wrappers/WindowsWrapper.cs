using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TelldusCoreWrapper.Wrappers
{
    internal class WindowsWrapper
    {
        private const string TELLDUS_CORE_DLL = "TelldusCore.dll";

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern void tdInit();

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdRegisterDeviceEvent(NativeWrapper.TDDeviceEvent eventFunction, IntPtr context);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdRegisterDeviceChangeEvent(NativeWrapper.TDDeviceChangeEvent eventFunction, IntPtr context);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdRegisterRawDeviceEvent(NativeWrapper.TDRawDeviceEvent eventFunction, IntPtr context);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdRegisterSensorEvent(NativeWrapper.TDSensorEvent eventFunction, IntPtr context);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdRegisterControllerEvent(NativeWrapper.TDControllerEvent eventFunction, IntPtr context);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern void tdClose();

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern void tdReleaseString(IntPtr thestring);


        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdTurnOn(int intDeviceId);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdTurnOff(int intDeviceId);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdLearn(int intDeviceId);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdMethods(int id, int methodsSupported);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdLastSentCommand(int intDeviceId, int methodsSupported);


        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdGetNumberOfDevices();

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdGetDeviceId(int intDeviceIndex);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdGetDeviceType(int intDeviceId);
        

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern IntPtr tdGetName(int intDeviceId);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern bool tdSetName(int intDeviceId, IntPtr chNewName);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern IntPtr tdGetProtocol(int intDeviceId);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern bool tdSetProtocol(int intDeviceId, IntPtr strProtocol);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern IntPtr tdGetModel(int intDeviceId);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern bool tdSetModel(int intDeviceId, IntPtr intModel);


        [DllImport(TELLDUS_CORE_DLL)]
        public static extern IntPtr tdGetDeviceParameter(int intDeviceId, IntPtr strName, IntPtr defaultValue);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern bool tdSetDeviceParameter(int intDeviceId, IntPtr strName, IntPtr strValue);


        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdAddDevice();

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern bool tdRemoveDevice(int intDeviceId);


        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdSensor(IntPtr protocol, int protocolLength, IntPtr model, int modelLength, IntPtr id, IntPtr dataTypes);

        [DllImport(TELLDUS_CORE_DLL)]
        public static extern int tdSensorValue(IntPtr protocol, IntPtr model, int id, int dataType, IntPtr value, int valueLength, IntPtr timestamp);
    }
}
