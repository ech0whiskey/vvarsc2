using System;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Authentication;
using System.Web.Http.Filters;

namespace vvarscNET.Web.API.Infrastructure.Attributes
{
    /// <summary>
    /// API Expcetion Handler Class
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exceptionType = actionExecutedContext.Exception.GetType();
            var statusCode = HttpStatusCode.InternalServerError;
            var message = actionExecutedContext.Exception.Message;

            if (exceptionType == typeof(AuthenticationException) || exceptionType == typeof(SecurityException))
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                statusCode = HttpStatusCode.Forbidden;
            }
            else if (exceptionType == typeof(ArgumentNullException) || exceptionType == typeof(ArgumentException))
            {
                statusCode = HttpStatusCode.BadRequest;
            }

            actionExecutedContext.Response = new HttpResponseMessage()
            {
                Content = new StringContent(message),
                StatusCode = statusCode
            };
            base.OnException(actionExecutedContext);
        }
    }

}
