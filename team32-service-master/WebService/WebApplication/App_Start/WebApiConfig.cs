using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Web.Http.Tracing;
using WebApplication.Code;

namespace WebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Services.Replace(typeof(ITraceWriter), new SimpleTracer());
            config.Filters.Add(new ValidateModelAttribute());
            // Adding Handler to the Pipeline
            config.MessageHandlers.Add(new MessageHandler());
            config.MessageHandlers.Add(new MethodOverrideHandler());
            config.MessageHandlers.Add(new CustomHeaderHandler());
            config.MessageHandlers.Add(new ApiKeyHandler("governor"));
            // Web API routes
            config.MapHttpAttributeRoutes();
            //config.Filters.Add(new AuthorizeAttribute()); // Authentication

            config.Routes.MapHttpRoute(
                $"DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );
        }
    }
}
