using vvarscNET.Core.Interfaces;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vvarscNET.Web.API.OwinMiddlewares.Configuration
{
    /// <summary>
    /// Global Exceptions Options
    /// </summary>
    public class GlobalExceptionOptions
    {
        private readonly Container _container;

        /// <summary>
        /// Global LogWriter
        /// </summary>
        public ILogWriter LogWriter { get { return _container.GetInstance<ILogWriter>(); } }

        /// <summary>
        /// constructor for GlobalExceptionOptions Class
        /// </summary>
        /// <param name="container"></param>
        public GlobalExceptionOptions(Container container)
        {
            _container = container;
        }
    }
}