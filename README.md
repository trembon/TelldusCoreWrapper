# Telldus Core Wrapper [![NuGet](https://img.shields.io/nuget/v/TelldusCoreWrapper.svg?style=flat-square)](https://www.nuget.org/packages/TelldusCoreWrapper/)
A wrapper for the Telldus Core written in C# for .NET Standard 2.0.

Sharing the code written for a home project of mine if anyone else could find this useful.

The code is developed on a Windows 10 machine and running production on a Raspberry Pi 3 with the latest Raspbian Stretch, with the Tellstick Duo controller.

The methods are created from the devices I currently have and my own requirements.
So I'm accepting request if more methods are wanted or just put them as pull requests.

## Telldus
To execute methods with the wrapper the Telldus Core service needs to be installed on the system.

### Windows
The TelldusCore service is installed with the TelldusCenter software, available here.

Link: [https://telldus.com/resources/](https://telldus.com/resources/)

### Linux
The TelldusCore service can be downloaded and compiled from Telldus own documentation site. This includes the tdtool command.

Link: [http://developer.telldus.se/wiki/TellStickInstallationSource](http://developer.telldus.se/wiki/TellStickInstallationSource)

## Examples

### Console application
```csharp
using (ITelldusCoreService service = new TelldusCoreService())
{
	service.Initialize();

	var list = service.GetDevices();
	foreach(var item in list)
		Console.WriteLine($"{item.ID}: {item.Name}");

	int newDeviceId = service.AddDevice("test 1", "arctech", "selflearning-switch", new Dictionary<string, string> { { "house", "953934" }, { "unit", "1" } });
	Console.WriteLine(newDeviceId);
    
	Device newDevice = service.GetDevice(newDeviceId);
	Console.WriteLine(newDevice.Name);
}
```

### ASP.NET Core
```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddMvc();
	
	...
	
	services.AddSingleton<ITelldusCoreService, TelldusCoreService>();
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env, ITelldusCoreService telldusCoreService)
{
	...
	
	telldusCoreService.InitializeInThread(3000);
}
```

```csharp
[Route("api/telldus")]
[ApiController]
public class TelldusAPIController : ControllerBase
{
	private ITelldusCoreService telldusCoreService;

	public TelldusAPIController(ITelldusCoreService telldusCoreService)
	{
		this.telldusCoreService = telldusCoreService;
	}

	[Route("send")]
	public IActionResult SendCommand(int deviceId, DeviceMethods command)
	{
		var result = telldusCoreService.SendCommand(deviceId, command);
		return Ok(result);
	}
}
```