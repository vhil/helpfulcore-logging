using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Helpfulcore.Logging.NLog
{
	public class NLogLoggingProvider : ILoggingProvider
	{
		protected Logger Logger;
		protected FileTarget FileTarget;

		public NLogLoggingProvider(string filePath)
		{
			var config = new LoggingConfiguration();
			this.FileTarget = new FileTarget
			{
				KeepFileOpen = true,
				Layout = @"${date:format=YYYY-DD-MM HH\:mm\:ss} ${logger} ${message}",
				FileName = filePath
			};

			config.AddTarget("Log file target", this.FileTarget);

			var rule2 = new LoggingRule("*", LogLevel.Debug, this.FileTarget);
			config.LoggingRules.Add(rule2);

			LogManager.Configuration = config;

			this.Logger = LogManager.GetLogger("NLogger");
		}

		public void Log(SeverityLevel level, string message, object owner, Exception exception = null, params object[] formatParams)
		{
			try
			{
				var msg = this.SafeFormat(message, formatParams);

				switch (level)
				{
					case SeverityLevel.Info:
						this.Logger.Info(msg);
						break;
					case SeverityLevel.Debug:
						this.Logger.Debug(msg);
						break;
					case SeverityLevel.Audit:
						this.Logger.Info("AUDIT: {0}", msg);
						break;
					case SeverityLevel.Error:
						this.Logger.Error(msg, exception, null);
						break;
					case SeverityLevel.Warn:
						this.Logger.Warn(msg, exception, null);
						break;
				}
			}
			catch
			{
				// ignored
			}
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
