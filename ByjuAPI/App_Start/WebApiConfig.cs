using ByjuAPI.Helper;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ByjuAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Enabling CORs in Web API
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "GET,POST,PUT");
            config.EnableCors(cors);

            // Registering the TokenValidation for JWT token validation
            config.MessageHandlers.Add(new TokenValidationHandler());

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
