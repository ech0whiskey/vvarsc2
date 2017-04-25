using vvarscNET.Core.Interfaces;
using vvarscNET.Web.API.Infrastructure.Exceptions;
using vvarscNET.Web.API.OwinMiddlewares;
using vvarscNET.Web.API.OwinMiddlewares.Configuration;
using Owin;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace vvarscNET.Web.API.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions for AppBuilder
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Method to configure use of ApiAuthenticationMiddleware
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IAppBuilder UseApiAuthentication(this IAppBuilder app, ApiAuthenticationOptions options)
        {
            app.Use<ApiAuthenticationMiddleware>(options);

            return app;
        }

        /// <summary>
        /// Method to configure use of ApiExceptionMiddleware
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IAppBuilder UseGlobalException(this IAppBuilder app, GlobalExceptionOptions options)
        {
            app.Use<GlobalExceptionMiddleware>(options);

            return app;
        }


    }

}
