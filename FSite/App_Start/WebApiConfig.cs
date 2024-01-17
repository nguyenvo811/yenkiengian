using MultipartDataMediaFormatter;
using MultipartDataMediaFormatter.Infrastructure;
using Newtonsoft.Json;
using System.Web.Http;
namespace FSite
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
          //  config.EnableCors();//Enable Allow Cross Origin Domain
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{action}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
           GlobalConfiguration.Configuration.Formatters.Add(new FormMultipartEncodedMediaTypeFormatter(new MultipartFormatterSettings()));
            jsonSerializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
        }
        private static JsonSerializerSettings jsonSerializerSettings;
        public static JsonSerializerSettings JsonSerializerSettings
        {
            get
            {
                return jsonSerializerSettings;
            }
            set
            {
                jsonSerializerSettings = value;
            }
        }
    }
}
