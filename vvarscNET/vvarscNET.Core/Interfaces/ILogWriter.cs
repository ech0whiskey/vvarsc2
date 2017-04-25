using vvarscNET.Core.Logger;

namespace vvarscNET.Core.Interfaces
{
    public interface ILogWriter
    {
        void Log(LogEntry logEntry);
    }
}
