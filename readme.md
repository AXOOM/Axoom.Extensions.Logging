Axoom.Extensions.Logging
======
This library is meant to be used for logging purposes on the AXOOM Platform.

Therefore it uses [NLog.Extensions.Logging](https://github.com/NLog/NLog.Extensions.Logging) as logprovider for `Microsoft.Extensions.Logging` and extends it with:

  * standardized AXOOM log layout
  * predefined AXOOM configuration
  * GELF logging format
  * extensions for `ILogger` which hide the parameter `eventId`
  * a nice way to configure logging

## Installation
You can add this library to your project using NuGet.

```
dotnet add package Axoom.Extensions.Logging
```

# Why is `GELF` the default logformat?
All AXOOM assets are running with docker. As we are collecting all logs in a centralized system, we need information about the running container which produces these logs. 

And here comes the problem with plain logs:  
Docker is not able to handle multi-line logs, e.g. if a log event contains a stackstrace, because it interprets each line as a new log event.
Therefore we've decided to produce json logs. To have a minimum set of information per log event we have decided to use the **Graylog Extended Log Format** ([GELF](http://docs.graylog.org/en/2.3/pages/gelf.html))

For development purposes you can switch the format to `Plain` for each `LogTarget`.

# How to use it
General usage of the Microsoft Logging Framework: [Introduction to Logging in .NETCore](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging)   

To use the AXOOM logging provider call the provider's extension method on an instance of `ILoggerFactory`, as shown in the following example:

```
public void ConfigureLogging(IServiceProvider serviceProvider)
{
    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
    loggerFactory.UseAxoomLogging("MyApplication", new LoggingOptions());
}
```

## Contributing

### Build
Run `build.ps1` to compile the source code and package the library as a NuGet package.
This script takes a version number as an input argument. The source code itself contains no version numbers. Instead version numbers should be determined at build time using [GitVersion](gitversion.readthedocs.io).

### Pull Requests
* Your pull request has to provide unit tests!
* Document any public interface method and property. At least:
  * Method: Summary, param
  * Property: Summary
