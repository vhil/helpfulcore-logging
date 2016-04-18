using System;

namespace Helpfulcore.Logging.Sc
{
    /// <summary>
    /// The default sitecore implementation of ILoggingProvider
    /// </summary>
    public class ScLoggingProvider : LoggingProvider
    {
        protected override void LogDebug(string message, object owner)
        {
            Sitecore.Diagnostics.Log.Debug(message, owner);
        }

        protected override void LogInfo(string message, object owner)
        {
            Sitecore.Diagnostics.Log.Info(message, owner);
        }

        protected override void LogAudit(string message, object owner)
        {
            Sitecore.Diagnostics.Log.Audit(message, owner);
        }

        protected override void LogWarn(string message, object owner, Exception exception = null)
        {
            if (exception == null)
            {
                Sitecore.Diagnostics.Log.Warn(message, owner);
            }
            else
            {
                Sitecore.Diagnostics.Log.Warn(message, exception, owner);
            }
        }

        protected override void LogError(string message, object owner, Exception exception = null)
        {
            if (exception == null)
            {
                Sitecore.Diagnostics.Log.Error(message, owner);
            }
            else
            {
                Sitecore.Diagnostics.Log.Error(message, exception, owner);
            }
        }
    }
}