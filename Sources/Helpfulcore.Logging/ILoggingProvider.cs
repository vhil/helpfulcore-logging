using System;

namespace Helpfulcore.Logging
{
    public interface ILoggingProvider
    {
        string LogLevel { get; }
        void Log(SeverityLevel level, string message, object owner, Exception exception = null, params object[] formatParams);
    }
}