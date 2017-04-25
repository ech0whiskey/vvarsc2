using System;

namespace vvarscNET.Core.Logger
{
    public enum LoggingEventType
    {
        Debug,
        Information,
        Warning,
        Error,
        Fatal
    };
    public class LogEntry
    {
        public readonly LoggingEventType Severity;
        public readonly string Message;
        public readonly string AppName;
        public readonly Exception Exception;

        public LogEntry(LoggingEventType severity, string message, string appName = "Api", Exception exception = null)
        {
            Message = message;
            Severity = severity;
            AppName = appName;
            Exception = exception;
        }

        public override string ToString()
        {
            return string.Format("{0}\tAppName: {1}", Message, AppName);
        }
    }
}
