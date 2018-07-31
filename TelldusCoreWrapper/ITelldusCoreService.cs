using System;
using System.Collections.Generic;
using TelldusCoreWrapper.Entities;
using TelldusCoreWrapper.Enums;

namespace TelldusCoreWrapper
{
    /// <summary>
    /// A wrapper for the Telldus Core library for communicating with the Telldus service.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface ITelldusCoreService : IDisposable
    {
        /// <summary>
        /// Occurs when a sensor sends an updated value to the Tellstick device.
        /// </summary>
        event EventHandler<SensorUpdateEventArgs> SensorUpdated;

        /// <summary>
        /// Occurs when a command is received for a device.
        /// Devices needs to be registered in your configuration to trigger this event.
        /// This event triggers when sending a command to one of your devices or a external source, like a remote, is used.
        /// </summary>
        event EventHandler<CommandReceivedEventArgs> CommandReceived;
        
        /// <summary>
        /// Occurs when a raw command is received.
        /// This event will trigger when receiving commands for devices that are not registered in your configuration.
        /// Unregistered devices that can trigger this event are for example a remote or a door sensor.
        /// </summary>
        event EventHandler<RawCommandReceivedEventArgs> RawCommandReceived;


        /// <summary>
        /// Initializes the Telldus library.
        /// This service needs to be initialized before other methods are called.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Initializes the Telldus library in a new thread.
        /// A thread will be needed to initialize in a different thread than the main thread, for example a AspNetCore project.
        /// The thread will be put in a suspended mode after initialization.
        /// This service needs to be initialized before other methods are called.
        /// </summary>
        void InitializeInThread();

        /// <summary>
        /// Initializes the Telldus library in a new thread with a delay (ms).
        /// A thread will be needed to initialize in a different thread than the main thread, for example a AspNetCore project.
        /// The thread will be put in a suspended mode after initialization.
        /// This service needs to be initialized before other methods are called.
        /// </summary>
        void InitializeInThread(int initializationDelay);


        /// <summary>
        /// Sends the command to the specified device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="command">The command.</param>
        /// <returns>Result of the command.</returns>
        ResultCode SendCommand(int deviceId, DeviceMethods command);

        /// <summary>
        /// Sends the command to the specified device, with a parameter.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="command">The command.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>Result of the command.</returns>
        ResultCode SendCommand(int deviceId, DeviceMethods command, string parameter);

        /// <summary>
        /// Turns the on the specified device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>Result of the command.</returns>
        ResultCode TurnOn(int deviceId);

        /// <summary>
        /// Turns the off the specified device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>Result of the command.</returns>
        ResultCode TurnOff(int deviceId);

        /// <summary>
        /// Sends the bell command to the specified device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>Result of the command.</returns>
        ResultCode Bell(int deviceId);

        /// <summary>
        /// Dims a device with the specified level (0-255).
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>Result of the command.</returns>
        ResultCode Dim(int deviceId, int level);

        /// <summary>
        /// Sends to learn command to the specified device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>Result of the command.</returns>
        ResultCode Learn(int deviceId);

        /// <summary>
        /// Gets the last command sent to the specified device.
        /// This method can be used to check if a device is turned on or off.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>The last command sent.</returns>
        DeviceMethods GetLastCommand(int deviceId);


        /// <summary>
        /// Gets a device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>The device.</returns>
        Device GetDevice(int deviceId);

        /// <summary>
        /// Gets all devices registered in the Telldus service.
        /// </summary>
        /// <returns>List of all devices.</returns>
        IEnumerable<Device> GetDevices();

        /// <summary>
        /// Gets all the sensors that the Telldus service have registered values from.
        /// </summary>
        /// <returns>List of all sensors.</returns>
        IEnumerable<Sensor> GetSensors();

        /// <summary>
        /// Gets the values for the specified sensor.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <returns>List of all values for the sensor.</returns>
        IEnumerable<SensorValue> GetSensorValues(Sensor sensor);
        

        /// <summary>
        /// Adds a device to the Telldus service.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="protocol">The protocol.</param>
        /// <param name="model">The model.</param>
        /// <param name="deviceParameters">The device parameters.</param>
        /// <returns>The ID of the added device.</returns>
        int AddDevice(string name, string protocol, string model, Dictionary<string, string> deviceParameters);

        /// <summary>
        /// Removes the specified device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>If removal was successfull.</returns>
        bool RemoveDevice(int deviceId);
    }
}
