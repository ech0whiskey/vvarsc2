namespace vvarscNET.Core.Logger
{
    public class ApiLogEntry
    {
        public readonly LoggingEventType Severity;
        public readonly string Message;

        public ApiLogEntry(LoggingEventType severity, string message)
        {
            Message = message;
            Severity = severity;
        }
    }
}
