using System;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Logger;

namespace vvarscNET.Core.Extensions
{
    public static class LoggerExtensions
    {
        public static void Log(this ILogWriter logWriter, string message, string appName, LoggingEventType eventType)
        {
            logWriter.Log(new LogEntry(eventType, message, appName));
        }

        public static void LogInformation(this ILogWriter logWriter, string message, string appName)
        {
            logWriter.Log(new LogEntry(LoggingEventType.Information, message, appName));
        }

        public static void LogError(this ILogWriter logWriter, string appName, Exception exception)
        {
            logWriter.Log(new LogEntry(LoggingEventType.Error, exception.Message, appName, exception));
        }
    }
}
