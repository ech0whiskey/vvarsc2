using vvarscNET.Core.Interfaces;
using vvarscNET.Web.API.Infrastructure.Exceptions;
using vvarscNET.Web.API.Infrastructure.Extensions;
using vvarscNET.Web.API.OwinMiddlewares.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using DalSoft.WebApi.HelpPage;

[assembly: OwinStartup(typeof(vvarscNET.Web.API.App_Start.Startup))]

namespace vvarscNET.Web.API.App_Start
{
    /// <summary>
    /// App StartUp Configuration Class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// StartUp Configuration Method
        /// </summary>
        /// <param name="appBuilder"></param>
        public void Configuration(IAppBuilder appBuilder)
        {
            // Enable Cors 
            appBuilder.UseCors(CorsOptions.AllowAll);

            // Creating an HttpConfiguration instance 
            var httpConfiguration = new HttpConfiguration();

            // Adding a Autherize Attribute
            httpConfiguration.Filters.Add(new AuthorizeAttribute());

            // Setting the Serialization Settings
            SerializeSettings(httpConfiguration);

            // Setting up the IoC container
            var container = IoCSetup(appBuilder, httpConfiguration);

            // Exception Middleware
            appBuilder.UseGlobalException(new GlobalExceptionOptions(container));

            // Exception Handler
            httpConfiguration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // Logger - catch unhandled exception in the controller and log them
            httpConfiguration.Services.Add(typeof(IExceptionLogger), new UnhandledExceptionLogger(container.GetInstance<ILogWriter>()));

            // Registering the Authorization Middleware
            appBuilder.UseApiAuthentication(new ApiAuthenticationOptions(container));

            appBuilder.UseWebApi(httpConfiguration);

            //using this nuget package for help page because it supports owin self serve that the integration tests are using
            //https://github.com/DalSoft/DalSoft.WebApi.HelpPage
            appBuilder.UseWebApiHelpPage(httpConfiguration);

            WebApiConfig.Register(httpConfiguration);
        }

        Container IoCSetup(IAppBuilder appBuilder, HttpConfiguration httpConfiguration)
        {
            var container = new Container();
            appBuilder.Use(async (context, next) =>
            {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
            SimpleInjectorWebApiInitializer.InitializeContainer(container);
            container.RegisterWebApiControllers(httpConfiguration);
            container.Verify();
            httpConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            return container;
        }

        /// <summary>
        /// Private method for adding json serializer settings
        /// </summary>
        /// <param name="config">HttpConfiguration</param>
        void SerializeSettings(HttpConfiguration config)
        {
            JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
            jsonSetting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings = jsonSetting;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
        }
    }
}
