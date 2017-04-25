using vvarscNET.Core.Extensions;
using vvarscNET.Core.Interfaces;
using System.Web.Http.ExceptionHandling;

namespace vvarscNET.Web.API.Infrastructure.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class UnhandledExceptionLogger : ExceptionLogger
    {
        private readonly ILogWriter _logWriter;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logWriter"></param>
        public UnhandledExceptionLogger(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Log(ExceptionLoggerContext context)
        {
            _logWriter.LogError("API", context.Exception);
        }
    }
}
