using Microsoft.Practices.Unity.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Unity;

namespace Nagarro.CensusPopulation.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Web API configuration and services
            //ReferenceLoopHandling.Ignore will solve the Self referencing loop detected error

            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //config.EnableCors();

            //Will serve json as default instead of XML

            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultRoute",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Filters.Add(new AuthorizeAttribute());
            //WebApiConfig.Register(config);

            var container = new UnityContainer();
            var unitySection = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            unitySection.Configure(container);
            var resolver = new UnityResolver(container);
            config.DependencyResolver = resolver;
        }
    }
}
