using System;

namespace Helpfulcore.Logging
{
    /// <summary>
    /// ILoggingManager - contract for logger manager implementation.
    /// </summary>
    public interface ILoggingService
    {
		/// <summary>
		/// Logs the specified level.
		/// </summary>
		/// <param name="level">The level.</param>
		/// <param name="message">The message.</param>
		/// <param name="owner">The owner.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="formatParams">The format parameters.</param>
        void Log(SeverityLevel level, string message, object owner, Exception exception = null, params object[] formatParams);

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="formatParams">The format parameters.</param>
        void Info(string message, object owner, params object[] formatParams);

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="formatParams">The format parameters.</param>
        void Warn(string message, object owner, Exception exception = null, params object[] formatParams);

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="formatParams">The format parameters.</param>
        void Debug(string message, object owner, params object[] formatParams);

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="formatParams">The format parameters.</param>
        void Error(string message, object owner, Exception exception = null, params object[] formatParams);

        /// <summary>
        /// Audits the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="formatParams">The format parameters.</param>
        void Audit(string message, object owner, params object[] formatParams);
    }
}
