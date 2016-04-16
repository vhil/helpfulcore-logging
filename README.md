### Helpfulcore - helpful features for Sitecore
# Helpfulcore Logging

Abstracted logging service to support multiple log providers. 

### Helpfulcore.Logging

This project declares the
```csharp
Helpfulcore.Logging.ILoggingService
```
interface which is meant to be the container for different provider implementation. Its implementation is
```csharp
Helpfulcore.Logging.LoggingService
```
Initialize your LoggingService with number of 
```csharp
Helpfulcore.Logging.ILoggingProvider
```
instances and use interface methods for writing to your logs into all specified providers.

## Providers