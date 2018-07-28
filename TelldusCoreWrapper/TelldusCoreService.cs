using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using TelldusCoreWrapper.Entities;
using TelldusCoreWrapper.Enums;
using TelldusCoreWrapper.Extensions;
using TelldusCoreWrapper.Wrappers;

namespace TelldusCoreWrapper
{
    /// <summary>
    /// A wrapper for the Telldus Core library for communicating with the Telldus service.
    /// </summary>
    /// <seealso cref="TelldusCoreWrapper.ITelldusCoreService" />
    public class TelldusCoreService : ITelldusCoreService
    {
        #region Constants
        /// <summary>
        /// The default initialization delay when initializing in a thread.
        /// </summary>
        private const int DEFAULT_INITIALIZATION_DELAY = 1000;

        /// <summary>
        /// The protocol string size.
        /// </summary>
        private const int PROTOCOL_STRING_SIZE = 20;
        /// <summary>
        /// The model string size.
        /// </summary>
        private const int MODEL_STRING_SIZE = 30;
        /// <summary>
        /// The value string size.
        /// </summary>
        private const int VALUE_STRING_SIZE = 20;

        /// <summary>
        /// The name the Telldus service will return if requesting a device that does not exist.
        /// </summary>
        private const string NOT_EXISTING_DEVICE_NAME = "UNKNOWN";
        #endregion

        private ManualResetEvent eventThread;

        private NativeWrapper.TDDeviceEvent deviceEvent;
        private NativeWrapper.TDDeviceChangeEvent deviceChangeEvent;
        private NativeWrapper.TDRawDeviceEvent rawDeviceEvent;
        private NativeWrapper.TDSensorEvent sensorEvent;
        private NativeWrapper.TDControllerEvent controllerEvent;

        /// <summary>
        /// Occurs when a sensor sends an updated value to the Tellstick device.
        /// </summary>
        public event EventHandler<SensorUpdateEventArgs> SensorUpdated;

        /// <summary>
        /// Occurs when a command is received for a device.
        /// Devices needs to be registered in your configuration to trigger this event.
        /// This event triggers when sending a command to one of your devices or a external source, like a remote, is used.
        /// </summary>
        public event EventHandler<CommandReceivedEventArgs> CommandReceived;

        /// <summary>
        /// Occurs when a raw command is received.
        /// This event will trigger when receiving commands for devices that are not registered in your configuration.
        /// Unregistered devices that can trigger this event are for example a remote or a door sensor.
        /// </summary>
        public event EventHandler<RawCommandReceivedEventArgs> RawCommandReceived;


        /// <summary>
        /// Initializes the Telldus library.
        /// This service needs to be initialized before other methods are called.
        /// </summary>
        public void Initialize()
        {
            NativeWrapper.tdInit();

            deviceEvent = TDDeviceEvent;
            deviceChangeEvent = TDDeviceChangeEvent;
            rawDeviceEvent = TDRawDeviceEvent;
            sensorEvent = TDSensorEvent;
            controllerEvent = TDControllerEvent;

            NativeWrapper.tdRegisterDeviceEvent(deviceEvent, IntPtr.Zero);
            NativeWrapper.tdRegisterDeviceChangeEvent(deviceChangeEvent, IntPtr.Zero);
            NativeWrapper.tdRegisterRawDeviceEvent(rawDeviceEvent, IntPtr.Zero);
            NativeWrapper.tdRegisterSensorEvent(sensorEvent, IntPtr.Zero);
            NativeWrapper.tdRegisterControllerEvent(controllerEvent, IntPtr.Zero);
        }

        /// <summary>
        /// Initializes the Telldus library in a new thread.
        /// A thread will be needed to initialize in a different thread than the main thread, for example a AspNetCore project.
        /// The thread will be put in a suspended mode after initialization.
        /// This service needs to be initialized before other methods are called.
        /// </summary>
        public void InitializeInThread()
        {
            InitializeInThread(DEFAULT_INITIALIZATION_DELAY);
        }

        /// <summary>
        /// Initializes the Telldus library in a new thread with a delay (ms).
        /// A thread will be needed to initialize in a different thread than the main thread, for example a AspNetCore project.
        /// The thread will be put in a suspended mode after initialization.
        /// This service needs to be initialized before other methods are called.
        /// </summary>
        /// <param name="initializationDelay"></param>
        public void InitializeInThread(int initializationDelay)
        {
            new Thread(() =>
            {
                Thread.Sleep(initializationDelay);

                Initialize();

                eventThread = new ManualResetEvent(false);
                eventThread.WaitOne();
            }).Start();
        }


        /// <summary>
        /// Sends the command to the specified device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="command">The command.</param>
        /// <returns>
        /// Result of the command.
        /// </returns>
        public ResultCode SendCommand(int deviceId, DeviceMethods command)
        {
            return SendCommand(deviceId, command, null);
        }

        /// <summary>
        /// Sends the command to the specified device, with a parameter.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="command">The command.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// Result of the command.
        /// </returns>
        public ResultCode SendCommand(int deviceId, DeviceMethods command, string parameter)
        {
            switch (command)
            {
                case DeviceMethods.TurnOff: return TurnOff(deviceId);
                case DeviceMethods.TurnOn: return TurnOn(deviceId);
                case DeviceMethods.Learn: return Learn(deviceId);

                default: return ResultCode.MethodNotSupported;
            }
        }

        /// <summary>
        /// Turns off the specified device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>
        /// Result of the command.
        /// </returns>
        public ResultCode TurnOff(int deviceId)
        {
            return (ResultCode)NativeWrapper.tdTurnOff(deviceId);
        }

        /// <summary>
        /// Turns on the specified device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>
        /// Result of the command.
        /// </returns>
        public ResultCode TurnOn(int deviceId)
        {
            return (ResultCode)NativeWrapper.tdTurnOn(deviceId);
        }

        /// <summary>
        /// Sends to learn command to the specified device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>
        /// Result of the command.
        /// </returns>
        public ResultCode Learn(int deviceId)
        {
            return (ResultCode)NativeWrapper.tdLearn(deviceId);
        }

        /// <summary>
        /// Gets the last command sent to the specified device.
        /// This method can be used to check if a device is turned on or off.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>
        /// The last command sent.
        /// </returns>
        public DeviceMethods GetLastCommand(int deviceId)
        {
            DeviceMethods deviceMethods = GetAllMethods();
            return (DeviceMethods)NativeWrapper.tdLastSentCommand(deviceId, (int)deviceMethods);
        }


        /// <summary>
        /// Gets a device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>
        /// The device.
        /// </returns>
        public Device GetDevice(int deviceId)
        {
            IntPtr nameIntPtr = NativeWrapper.tdGetName(deviceId);
            string name = Marshal.PtrToStringAnsi(nameIntPtr);
            NativeWrapper.tdReleaseString(nameIntPtr);

            DeviceMethods allMethods = GetAllMethods();
            DeviceMethods supportedMethods = (DeviceMethods)NativeWrapper.tdMethods(deviceId, (int)allMethods);

            if (name == NOT_EXISTING_DEVICE_NAME && supportedMethods == 0)
                return null;

            IntPtr modelIntPtr = NativeWrapper.tdGetModel(deviceId);
            string model = Marshal.PtrToStringAnsi(modelIntPtr);
            NativeWrapper.tdReleaseString(modelIntPtr);

            IntPtr protocolIntPtr = NativeWrapper.tdGetProtocol(deviceId);
            string protocol = Marshal.PtrToStringAnsi(protocolIntPtr);
            NativeWrapper.tdReleaseString(protocolIntPtr);

            return new Device
            {
                ID = deviceId,
                Name = name,
                Model = model,
                Protocol = protocol,
                SupportedMethods = supportedMethods
            };
        }

        /// <summary>
        /// Gets all devices registered in the Telldus service.
        /// </summary>
        /// <returns>
        /// List of all devices.
        /// </returns>
        public IEnumerable<Device> GetDevices()
        {
            int numberOfDevices = NativeWrapper.tdGetNumberOfDevices();
            for (int i = 0; i < numberOfDevices; i++)
            {
                int deviceId = NativeWrapper.tdGetDeviceId(i);
                yield return GetDevice(deviceId);
            }
        }

        /// <summary>
        /// Gets all the sensors that the Telldus service have registered values from.
        /// </summary>
        /// <returns>
        /// List of all sensors.
        /// </returns>
        public IEnumerable<Sensor> GetSensors()
        {
            IntPtr protocol = Marshal.AllocHGlobal(Marshal.SystemDefaultCharSize * PROTOCOL_STRING_SIZE);
            IntPtr model = Marshal.AllocHGlobal(Marshal.SystemDefaultCharSize * MODEL_STRING_SIZE);
            IntPtr id = Marshal.AllocHGlobal(sizeof(int));
            IntPtr dataType = Marshal.AllocHGlobal(sizeof(int));

            while (NativeWrapper.tdSensor(protocol, PROTOCOL_STRING_SIZE, model, MODEL_STRING_SIZE, id, dataType) == (int)ResultCode.Success)
            {
                yield return new Sensor
                {
                    ID = Marshal.ReadInt32(id),
                    Protocol = Marshal.PtrToStringAnsi(protocol),
                    Model = Marshal.PtrToStringAnsi(model),
                    Sensors = (SensorValueType)Marshal.ReadInt32(dataType)
                };
            }

            Marshal.FreeHGlobal(protocol);
            Marshal.FreeHGlobal(model);
            Marshal.FreeHGlobal(id);
            Marshal.FreeHGlobal(dataType);
        }

        /// <summary>
        /// Gets the values for the specified sensor.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <returns>
        /// List of all values for the sensor.
        /// </returns>
        public IEnumerable<SensorValue> GetSensorValues(Sensor sensor)
        {
            IntPtr protocol = new IntPtr();
            IntPtr model = new IntPtr();
            IntPtr value = Marshal.AllocHGlobal(Marshal.SystemDefaultCharSize * VALUE_STRING_SIZE);
            IntPtr timestamp = Marshal.AllocHGlobal(sizeof(int));

            foreach (SensorValueType sensorValueType in Enum.GetValues(typeof(SensorValueType)))
            {
                if (!sensor.Sensors.HasFlag(sensorValueType))
                    continue;

                protocol = Marshal.StringToHGlobalAnsi(sensor.Protocol);
                model = Marshal.StringToHGlobalAnsi(sensor.Model);

                NativeWrapper.tdSensorValue(protocol, model, sensor.ID, (int)sensorValueType, value, VALUE_STRING_SIZE, timestamp);

                yield return new SensorValue
                {
                    SensorID = sensor.ID,
                    Type = sensorValueType,
                    Value = Marshal.PtrToStringAnsi(value),
                    Timestamp = Marshal.ReadInt32(timestamp).ToLocalDateTime()
                };
            }

            Marshal.FreeHGlobal(value);
            Marshal.FreeHGlobal(timestamp);
            Marshal.FreeHGlobal(protocol);
            Marshal.FreeHGlobal(model);
        }


        /// <summary>
        /// Adds a device to the Telldus service.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="protocol">The protocol.</param>
        /// <param name="model">The model.</param>
        /// <param name="deviceParameters">The device parameters.</param>
        /// <returns>
        /// The ID of the added device.
        /// </returns>
        public int AddDevice(string name, string protocol, string model, Dictionary<string, string> deviceParameters)
        {
            int deviceId = NativeWrapper.tdAddDevice();
            try
            {
                // set the name
                IntPtr nameIntPtr = Marshal.StringToHGlobalAnsi(name);
                NativeWrapper.tdSetName(deviceId, nameIntPtr);
                Marshal.FreeHGlobal(nameIntPtr);

                // set the protocol
                IntPtr protocolIntPtr = Marshal.StringToHGlobalAnsi(protocol);
                NativeWrapper.tdSetProtocol(deviceId, protocolIntPtr);
                Marshal.FreeHGlobal(protocolIntPtr);

                // set the model
                IntPtr modelIntPtr = Marshal.StringToHGlobalAnsi(model);
                NativeWrapper.tdSetModel(deviceId, modelIntPtr);
                Marshal.FreeHGlobal(modelIntPtr);

                // set all device parameter
                if (deviceParameters != null && deviceParameters.Count > 0)
                {
                    foreach (var kvp in deviceParameters)
                    {
                        IntPtr keyIntPtr = Marshal.StringToHGlobalAnsi(kvp.Key);
                        IntPtr valueIntPtr = Marshal.StringToHGlobalAnsi(kvp.Value);

                        NativeWrapper.tdSetDeviceParameter(deviceId, keyIntPtr, valueIntPtr);

                        Marshal.FreeHGlobal(keyIntPtr);
                        Marshal.FreeHGlobal(valueIntPtr);
                    }
                }
            }
            catch
            {
                NativeWrapper.tdRemoveDevice(deviceId);
                return -1;
            }

            return deviceId;
        }

        /// <summary>
        /// Removes the specified device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>
        /// If removal was successfull.
        /// </returns>
        public bool RemoveDevice(int deviceId)
        {
            return NativeWrapper.tdRemoveDevice(deviceId);
        }

        #region Event delegate methods
        private void TDDeviceEvent(int deviceId, int method, IntPtr data, int callbackId, IntPtr context)
        {
            string parsedData = Marshal.PtrToStringAnsi(data);

            Device device = GetDevice(deviceId);
            if (device == null)
                return;

            CommandReceivedEventArgs eventArgs = new CommandReceivedEventArgs(device, (DeviceMethods)method, parsedData);
            CommandReceived.Trigger(this, eventArgs);
        }

        private void TDDeviceChangeEvent(int deviceId, int changeEvent, int changeType, int callbackId, IntPtr context)
        {
            // TODO: implement?
        }

        private void TDRawDeviceEvent(IntPtr data, int controllerId, int callbackId, IntPtr context)
        {
            string parsedData = Marshal.PtrToStringAnsi(data);
            if (string.IsNullOrWhiteSpace(parsedData))
                return;

            RawCommandReceivedEventArgs eventArgs = new RawCommandReceivedEventArgs(controllerId, parsedData);
            RawCommandReceived.Trigger(this, eventArgs);
        }

        private void TDSensorEvent(IntPtr protocol, IntPtr model, int id, int dataType, IntPtr value, int timestamp, int callbackId, IntPtr context)
        {
            string parsedProtocol = Marshal.PtrToStringAnsi(protocol);
            string parsedModel = Marshal.PtrToStringAnsi(model);
            string parsedValue = Marshal.PtrToStringAnsi(value);

            SensorUpdateEventArgs eventArgs = new SensorUpdateEventArgs
            {
                Value = new SensorValue
                {
                    SensorID = id,
                    Type = (SensorValueType)dataType,
                    Value = parsedValue,
                    Timestamp = timestamp.ToLocalDateTime()
                }
            };

            SensorUpdated.Trigger(this, eventArgs);
        }

        private void TDControllerEvent(int controllerId, int changeEvent, int changeType, IntPtr newValue, int callbackId, IntPtr context)
        {
            string parsedNewValue = Marshal.PtrToStringAnsi(newValue);
            // TODO: implement?
        }
        #endregion

        #region Helper methods
        private DeviceMethods GetAllMethods()
        {
            int allMethods = 0;
            var availableMethods = Enum.GetValues(typeof(DeviceMethods));
            foreach (int method in availableMethods)
                allMethods = allMethods | (int)method;

            return (DeviceMethods)allMethods;
        }
        #endregion

        #region Interface - IDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            NativeWrapper.tdClose();

            if (eventThread != null)
                eventThread.Set();
        }
        #endregion
    }
}
