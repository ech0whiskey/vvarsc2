using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace vvarscNET.Web.API.Infrastructure.Attributes
{
    /// <summary>
    /// API Validation Class
    /// </summary>
    public class ApiModelValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Override Method for API Request Model Validation
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}