using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Helpfulcore.Logging.NLog
{
	public class NLogLoggingProvider : LoggingProvider
    {
        private static readonly object SyncRoot = new object();
            
		protected Logger Logger;

		public NLogLoggingProvider(string filePath)
		{
		    lock (SyncRoot)
		    {
		        filePath = filePath.Replace("/", "\\");
		        var loggerName = GenerateLoggerName(filePath);
		        var targetName = "Target_" + loggerName;

                if (LogManager.Configuration == null)
		        {
		            LogManager.Configuration = new LoggingConfiguration();
		        }

		        var config = LogManager.Configuration;
		        var fileTarget = new FileTarget
		        {
		            KeepFileOpen = true,
		            Layout = @"${longdate} ${level:uppercase=true:padding=6} ${message}",
		            FileName = filePath,
		            Name = targetName
                };

		        config.AddTarget(targetName, fileTarget);
		        config.LoggingRules.Add(new LoggingRule(loggerName, global::NLog.LogLevel.Debug, fileTarget));

		        LogManager.Configuration = config;
		        this.Logger = LogManager.GetLogger(loggerName);
		    }
		}

	    private static string GenerateLoggerName(string filePath)
	    {
	        var loggerName = "NLogger_" + filePath
                .Replace("\\", "_")
                .Replace("{", "")
                .Replace("}", "")
                .Replace("=", "")
                .Replace("$", "")
                .Replace("*", "")
                .Replace(".", "_")
                .Replace(":", "");

	        return loggerName;
	    }

	    protected override void LogDebug(string message, object owner)
	    {
            this.Logger.Debug(message);
        }

	    protected override void LogInfo(string message, object owner)
	    {
	        this.Logger.Info(message);
	    }

	    protected override void LogAudit(string message, object owner)
	    {
            this.Logger.Info(message);
        }

	    protected override void LogWarn(string message, object owner, Exception exception = null)
	    {
            if (exception != null)
            {
                this.Logger.Warn(exception, message);
            }
            else
            {
                this.Logger.Warn(message);
            }
        }

	    protected override void LogError(string message, object owner, Exception exception = null)
	    {
            if (exception != null)
            {
                this.Logger.Error(exception, message);
            }
            else
            {
                this.Logger.Error(message);
            }
        }
    }
}