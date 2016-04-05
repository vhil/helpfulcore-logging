using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Helpfulcore.Logging.NLog
{
	public class NLogLoggingProvider : ILoggingProvider
	{
		protected Logger Logger;

		public NLogLoggingProvider(string filePath)
		{
			var config = new LoggingConfiguration();
			var fileTarget = new FileTarget
			{
				KeepFileOpen = true,
				Layout = @"${longdate} ${level:uppercase=true:padding=5} ${message}",
				FileName = filePath
			};

			config.AddTarget("Log file target", fileTarget);
			config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));

			LogManager.Configuration = config;

			this.Logger = LogManager.GetLogger("NLogger");
		}

		public void Log(SeverityLevel level, string message, object owner, Exception exception = null, params object[] formatParams)
		{
			try
			{
				var msg = this.GetMessage(message, owner, formatParams);

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

		private string GetMessage(string message, object owner, object[] formatParams)
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
