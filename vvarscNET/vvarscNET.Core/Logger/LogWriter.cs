using log4net;
using vvarscNET.Core.Interfaces;

namespace vvarscNET.Core.Logger
{
    /// <summary>
    /// 
    /// </summary>
    public class LogWriter : ILogWriter
    {
        private readonly ILog _log;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public LogWriter(ILog logger)
        {
            _log = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logEntry"></param>
        public void Log(LogEntry logEntry)
        {
            switch (logEntry.Severity)
            {
                case LoggingEventType.Information:
                    _log.Info(logEntry);
                    break;
                case LoggingEventType.Debug:
                    _log.Debug(logEntry);
                    break;
                case LoggingEventType.Warning:
                    _log.Warn(logEntry);
                    break;
                case LoggingEventType.Error:
                    _log.Error(logEntry, logEntry.Exception);
                    break;
                case LoggingEventType.Fatal:
                    _log.Fatal(logEntry, logEntry.Exception);
                    break;
            }
        }
    }
}
