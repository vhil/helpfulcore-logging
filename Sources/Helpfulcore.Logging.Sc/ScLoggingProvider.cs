using System;

namespace Helpfulcore.Logging.Sc
{
    /// <summary>
    /// The default sitecore implementation of ILoggingProvider
    /// </summary>
    public class ScLoggingProvider : ILoggingProvider
    {
        /// <summary>
        /// Logs the specified level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="formatParams">The format parameters.</param>
        public void Log(SeverityLevel level, string message, object owner, Exception exception = null, params object[] formatParams)
        {
            try
            {
                var msg = GetMessage(message, owner, formatParams);

                var ownerParam = owner ?? "NULL";

                switch (level)
                {
                    case SeverityLevel.Info:
                        Sitecore.Diagnostics.Log.Info(msg, ownerParam);
                        break;
                    case SeverityLevel.Debug:
                        Sitecore.Diagnostics.Log.Debug(msg, ownerParam);
                        break;
                    case SeverityLevel.Audit:
                        Sitecore.Diagnostics.Log.Audit(msg, ownerParam);
                        break;
                    case SeverityLevel.Error:
                        if (exception == null)
                        {
                            Sitecore.Diagnostics.Log.Error(msg, ownerParam);
                        }
                        else
                        {
                            Sitecore.Diagnostics.Log.Error(msg, exception, ownerParam);
                        }
                        break;
                    case SeverityLevel.Warn:
                        if (exception == null)
                        {
                            Sitecore.Diagnostics.Log.Warn(msg, ownerParam);
                        }
                        else
                        {
                            Sitecore.Diagnostics.Log.Warn(msg, exception, ownerParam);

                        }
                        break;
                }
            }
            catch
            {
	            // ignored
            }
        }

        private static string GetMessage(string message, object owner, object[] formatParams)
        {
            var ownerType = "NULL";

            if (owner != null)
            {
                var type = owner as Type;
                ownerType = type == null ? owner.GetType().Name : type.Name;
            }

            var msg = "[" + ownerType + "]: " + SafeFormat(message, formatParams);
            return msg;
        }

        private static string SafeFormat(string message, object[] format)
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