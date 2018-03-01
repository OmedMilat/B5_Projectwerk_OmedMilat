using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Routing;

namespace HenrikMotors.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
            //       .Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept",
            //                  "text/html",
            //                  StringComparison.InvariantCultureIgnoreCase,
            //                  true,
            //                  "application/json"));
        }
    }
}
