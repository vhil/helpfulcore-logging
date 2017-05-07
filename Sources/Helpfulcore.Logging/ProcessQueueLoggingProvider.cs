namespace Helpfulcore.Logging
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class ProcessQueueLoggingProvider : ILoggingProvider
    {
        public const string CompletedKeyword = "##Completed##";
        private ConcurrentQueue<string> progressQueue = new ConcurrentQueue<string>();

        public ProcessQueueLoggingProvider()
        {
            this.LogLevel = SeverityLevel.Debug.ToString();
        }

        public string LogLevel { get; protected set; }

        public void Clear()
        {
            this.progressQueue = new ConcurrentQueue<string>();
        }

        public void EndLogging()
        {
            this.Log(SeverityLevel.Info, CompletedKeyword, this);
        }

        public string[] GetLogMessages(int count = 100)
        {
            var dequeued = new List<string>();

            for (var i = 0; i < count; i++)
            {
                string message;
                if (!this.progressQueue.TryDequeue(out message))
                {
                    break;
                }

                if (message != null)
                {
                    dequeued.Add(message);
                }
            }

            return dequeued.ToArray();
        }

        public virtual void Log(SeverityLevel level, string message, object owner, Exception exception = null, params object[] formatParams)
        {
            try
            {
                if (message == CompletedKeyword)
                {
                    this.progressQueue.Enqueue(CompletedKeyword);
                    return;
                }

                SeverityLevel logLevel;
                if (!Enum.TryParse(this.LogLevel, out logLevel))
                {
                    logLevel = SeverityLevel.Debug;
                }

                if (logLevel >= level)
                {
                    var msg = this.GetMessage(message, owner, logLevel, formatParams);

                    this.progressQueue.Enqueue(msg);
                }
            }
            catch
            {
                // ignored
            }
        }

        protected string GetMessage(string message, object owner, SeverityLevel logLevel, object[] formatParams)
        {
            var ownerType = "NULL";

            if (owner != null)
            {
                var type = owner as Type;
                ownerType = type == null ? owner.GetType().Name : type.Name;
            }

            var msg = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {logLevel.ToString().ToUpper()} [{ownerType}] {this.SafeFormat(message, formatParams)}";
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
