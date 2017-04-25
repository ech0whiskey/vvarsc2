using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace vvarscNET.Web.API.Infrastructure.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            // To Access Exception. ex  var exception = context.Exception;
            const string errorMessage = "An unexpected error occured";
            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                new
                {
                    Message = errorMessage
                });

            response.Headers.Add("X-Error", errorMessage);
            context.Result = new ResponseMessageResult(response);
            return base.HandleAsync(context, cancellationToken);
        }
    }
}
