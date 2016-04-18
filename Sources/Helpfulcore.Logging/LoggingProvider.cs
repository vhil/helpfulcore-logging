using System;

namespace Helpfulcore.Logging
{
    public abstract class LoggingProvider : ILoggingProvider
    {
        protected LoggingProvider()
        {
            this.LogLevel = SeverityLevel.Debug.ToString();
        }

        public string LogLevel { get; protected set; }

        public virtual void Log(SeverityLevel level, string message, object owner, Exception exception = null, params object[] formatParams)
        {
            try
            {
                var msg = this.GetMessage(message, owner, formatParams);
                var ownerParam = owner ?? "NULL";
                SeverityLevel logLevel;

                if (!Enum.TryParse(this.LogLevel, out logLevel))
                {
                    logLevel = SeverityLevel.Debug;
                }

                if (logLevel >= level)
                {
                    switch (level)
                    {
                        case SeverityLevel.Info:
                            this.LogInfo(msg, ownerParam);
                            break;
                        case SeverityLevel.Debug:
                            this.LogDebug(msg, ownerParam);
                            break;
                        case SeverityLevel.Audit:
                            this.LogAudit(string.Format("AUDIT: {0}", msg), ownerParam);
                            break;
                        case SeverityLevel.Error:
                            this.LogError(msg, ownerParam, exception);
                            break;
                        case SeverityLevel.Warn:
                            this.LogWarn(msg, ownerParam, exception);
                            break;
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        protected abstract void LogDebug(string message, object owner);
        protected abstract void LogInfo(string message, object owner);
        protected abstract void LogAudit(string message, object owner);
        protected abstract void LogWarn(string message, object owner, Exception exception = null);
        protected abstract void LogError(string message, object owner, Exception exception = null);
        
        protected string GetMessage(string message, object owner, object[] formatParams)
        {
            var ownerType = "NULL";

            if (owner != null)
            {
                var type = owner as Type;
                ownerType = type == null ? owner.GetType().Name : type.Name;
            }

            var msg = "[" + ownerType + "]: " + this.SafeFormat(message, formatParams);
            return msg;
        }

        protected string SafeFormat(string message, object[] format)
        {
            if (format != null && format.Length > 0)
            {
                for (var i = 0; i < format.Length; i++)
                {
                    var formatKey = string.Format("{{{0}}}", i);

                    if (message.Contains(formatKey))
                    {
                        message = message.Replace(formatKey, format[i].ToString());
                    }
                }
            }

            return message;
        }
    }
}