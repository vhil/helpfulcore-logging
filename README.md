### Helpfulcore - helpful features for Sitecore
# Helpfulcore Logging

Abstracted logging service to support multiple log providers. 

### Helpfulcore.Logging

This project declares the
```cs
Helpfulcore.Logging.ILoggingService
```
interface which is meant to be the container for different provider implementation. Its implementation is
```cs
Helpfulcore.Logging.LoggingService
```
Initialize your LoggingService with number of 
```cs
Helpfulcore.Logging.ILoggingProvider
```
instances and use interface methods for writing to your logs into all specified providers.

## Providers

There are 2 implementation to use currently:
* Helpfulcore.Logging.Sc - provider which uses Sitecore.Diagnostics.Log 
* Helpfulcore.Logging.NLog - provider which uses NLog file logger.

### Helpfulcore.Logging.Sc

In order to use in your Sitecore website solution please install next nuget packages to your Website solution:
```
Install-Package Helpfulcore.Logging.Sc
```
This package will install the Sitecore provider and will add the configuration for the LoggingService with one provider which just writes to Sitecore.Diagnostics.Log.
The configuration will reside in dedicated include config file /App_Config/Include/Helpfulcore/Helpfulcore.Logging.Sc.config
```xml
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
  <sitecore>
    <helpfulcore>
      <logging>
        <loggingService type="Helpfulcore.Logging.LoggingService, Helpfulcore.Logging" singleInstance="true" >
          <param name="provider1" ref="helpfulcore/logging/providers/scLoggingProvider"/>
        </loggingService>
        <providers>
          <scLoggingProvider type="Helpfulcore.Logging.Sc.ScLoggingProvider, Helpfulcore.Logging.Sc" />
        </providers>
      </logging>
    </helpfulcore>
  </sitecore>
</configuration>
```
In order to use it you can call it directly from Sitecore configuration factory
```cs
var loggingService = Factory.CreateObject("helpfulcore/logging/loggingService", true) as ILoggingService;
```
or register it in your IoC container like
```cs
container.Register(
	typeof(ILoggingService),
	() => Factory.CreateObject("helpfulcore/logging/loggingService", true) as ILoggingService,
	Lifestyle.Singleton);
```

### Helpfulcore.Logging.NLog
In order to use this provider in your solution, please install nuget package:
```
Install-Package Helpfulcore.Logging.NLog.Sc
```
This package will install the NLog provider and will add it to the LoggingService as second provider injected into constructor. Installing both packages leads to having single logging service which writes to both providers.
The configuration will reside in dedicated include config file /App_Config/Include/Helpfulcore/Helpfulcore.Logging.NLog.config
The default configuration will write to your $(dataFolder)/logs/Website.log.${date:format=yyyyMMdd}.txt file.
```xml
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
  <sitecore>
    <helpfulcore>
      <logging>
        <loggingService type="Helpfulcore.Logging.LoggingService, Helpfulcore.Logging" singleInstance="true" >
          <param name="provider2" ref="helpfulcore/logging/providers/nlogLoggingProvider"/>
        </loggingService>
        <providers>
          <nlogLoggingProvider type="Helpfulcore.Logging.NLog.NLogLoggingProvider, Helpfulcore.Logging.NLog" filePath="$(dataFolder)/logs/Website.log.${date:format=yyyyMMdd}.txt">
            <param name="filePath">$(filePath)</param>
          </nlogLoggingProvider>
        </providers>
      </logging>
    </helpfulcore>
  </sitecore>
</configuration>
```