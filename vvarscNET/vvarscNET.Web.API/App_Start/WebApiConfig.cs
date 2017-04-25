using vvarscNET.Web.API.Infrastructure.Attributes;
using System.Web.Http;

namespace vvarscNET.Web.API
{
    /// <summary>
    /// WebAPI Config Class
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register Config Entries - do not put routes in here, they are defined as attributes of each controller method.
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            log4net.Config.XmlConfigurator.Configure();

            // Web API configuration and services
            config.EnableCors();

            //ExceptionHandler Filters
            config.Filters.Add(new ApiExceptionFilterAttribute());

            // Action Filters
            config.Filters.Add(new ApiModelValidationAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
