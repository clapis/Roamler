using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Roamler.Web.Filters;

namespace Roamler.Web
{
    public static class WebApiConfig
    {
        public static HttpConfiguration ConfigureWebApi(this HttpConfiguration config)
        {
            // Register routes
            RegisterRoutes(config);

            // Register filters
            config.Filters.Add(new ModelValidationAttribute());

            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return config;
        }

        private static void RegisterRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "Default",
            //    routeTemplate: "{controller}/{id}",
            //    defaults: new { controller = "Home", id = RouteParameter.Optional }
            //);
        }
    }
}