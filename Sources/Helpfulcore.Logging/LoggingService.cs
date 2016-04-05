using System;

namespace Helpfulcore.Logging
{
    /// <summary>
    /// The Logging Manager
    /// </summary>
    public class LoggingService : ILoggingService
    {
        private readonly ILoggingProvider[] loggingProviders;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggingService"/> class.
		/// </summary>
		/// <param name="provider">The provider.</param>
		public LoggingService(ILoggingProvider provider)
			:this(new []{provider})
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggingService"/> class.
		/// </summary>
		/// <param name="provider1">The provider1.</param>
		/// <param name="provider2">The provider2.</param>
		public LoggingService(ILoggingProvider provider1, ILoggingProvider provider2)
			:this(new[] { provider1, provider2 })
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggingService"/> class.
		/// </summary>
		/// <param name="provider1">The provider1.</param>
		/// <param name="provider2">The provider2.</param>
		/// <param name="provider3">The provider3.</param>
		public LoggingService(ILoggingProvider provider1, ILoggingProvider provider2, ILoggingProvider provider3)
			: this(new[] { provider1, provider2, provider3 })
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggingService"/> class.
		/// </summary>
		/// <param name="provider1">The provider1.</param>
		/// <param name="provider2">The provider2.</param>
		/// <param name="provider3">The provider3.</param>
		/// <param name="provider4">The provider4.</param>
		public LoggingService(ILoggingProvider provider1, ILoggingProvider provider2, ILoggingProvider provider3, ILoggingProvider provider4)
			: this(new[] { provider1, provider2, provider3, provider4 })
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingService"/> class.
        /// </summary>
        /// <param name="providers">The providers.</param>
        public LoggingService(params ILoggingProvider [] providers)
        {
            this.loggingProviders = providers;
        }

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
            foreach (var provider in this.loggingProviders)
            {
                provider.Log(level, message, owner, exception, formatParams);
            }
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="formatParams">The format parameters.</param>
        public void Info(string message, object owner, params object[] formatParams)
        {
            foreach (var provider in this.loggingProviders)
            {
                provider.Log(SeverityLevel.Info, message, owner, null, formatParams);
            }
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="formatParams">The format parameters.</param>
        public void Warn(string message, object owner, Exception exception = null, params object[] formatParams)
        {
            foreach (var provider in this.loggingProviders)
            {
                provider.Log(SeverityLevel.Warn, message, owner, exception, formatParams);
            }
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="formatParams">The format parameters.</param>
        public void Debug(string message, object owner, params object[] formatParams)
        {
            foreach (var provider in this.loggingProviders)
            {
                provider.Log(SeverityLevel.Debug, message, owner, null, formatParams);
            }
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="formatParams">The format parameters.</param>
        public void Error(string message, object owner, Exception exception = null, params object[] formatParams)
        {
            foreach (var provider in this.loggingProviders)
            {
                provider.Log(SeverityLevel.Error, message, owner, exception, formatParams);
            }
        }

        /// <summary>
        /// Audits the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="formatParams">The format parameters.</param>
        public void Audit(string message, object owner, params object[] formatParams)
        {
            foreach (var provider in this.loggingProviders)
            {
                provider.Log(SeverityLevel.Audit, message, owner, null, formatParams);
            }
        }
    }
}
