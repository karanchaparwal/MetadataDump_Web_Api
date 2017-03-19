using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MetadataDump_Web_Api
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
            config.Routes.MapHttpRoute(
         name: "json",
          routeTemplate: "api/{controller}",
          defaults: new { Controllers = "CRM", Moviee = RouteParameter.Optional }
     );
        }
    }
}
