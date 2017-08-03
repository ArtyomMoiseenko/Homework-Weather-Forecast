using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Homework.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            config.EnableCors(cors);
            // Json view
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            //config.Formatters.JsonFormatter.MediaTypeMappings
            //    .Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept", "text/html",System.StringComparison.InvariantCultureIgnoreCase,
            //    true, "application/json"));
        }
    }
}