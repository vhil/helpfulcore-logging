using System;
using System.Configuration;
using System.IO;
using log4net;
using log4net.Config;

namespace Helpfulcore.Logging.Log4Net
{
    public class Log4NetLoggingProvider : ILoggingProvider
    {
        public Log4NetLoggingProvider(string configFilePath)
        {
			if (!string.IsNullOrEmpty(configFilePath) && File.Exists(configFilePath))
            {
                var fileInfo = new FileInfo(configFilePath);
                XmlConfigurator.ConfigureAndWatch(fileInfo);
            }
            else
            {
				throw new ConfigurationErrorsException(string.Format("Configuration file for Log4NetLoggingProvider by path '{0}' not found.", configFilePath));
            }
        }

        public void Log(SeverityLevel level, string message, object owner, Exception exception = null, params object[] formatParams)
        {
            try
            {
                var logger = this.GetLogger(owner);
                var msg = SafeFormat(message, formatParams);

                switch (level)
                {
                    case SeverityLevel.Info:
                        logger.Info(msg);
                        break;
                    case SeverityLevel.Debug:
                        logger.Debug(msg);
                        break;
                    case SeverityLevel.Audit:
                        logger.Info(string.Format("AUDIT: {0}", msg));
                        break;
                    case SeverityLevel.Error:
                        logger.Error(msg, exception);
                        break;
                    case SeverityLevel.Warn:
                        logger.Warn(msg, exception);
                        break;
                }
            }
            catch
            {
	            // ignored
            }
        }

        public virtual ILog GetLogger(object owner)
        {
            if (owner != null)
            {
                var type = owner as Type;
                return type != null ? LogManager.GetLogger(type) : LogManager.GetLogger(owner.GetType());
            }

            return LogManager.GetLogger("NULL");
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