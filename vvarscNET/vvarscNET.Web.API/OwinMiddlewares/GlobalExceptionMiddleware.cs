using vvarscNET.Core.Extensions;
using vvarscNET.Core.Interfaces;
using vvarscNET.Web.API.OwinMiddlewares.Configuration;
using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace vvarscNET.Web.API.OwinMiddlewares
{
    /// <summary>
    /// This class defines the GlobalException Middleware from Owin
    /// </summary>
    public class GlobalExceptionMiddleware : OwinMiddleware
    {
        GlobalExceptionOptions _options;

        /// <summary>
        /// constructor for GlobalExceptionMiddleware class
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public GlobalExceptionMiddleware(OwinMiddleware next, GlobalExceptionOptions options) : base(next)
        {
            _options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception ex)
            {
                _options.LogWriter.LogError("API", ex);

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ReasonPhrase = "Bad Request";
                context.Response.Write(ex.Message);
            }
        }
    }
}