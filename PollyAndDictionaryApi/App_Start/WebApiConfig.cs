using System.Web.Http;
using PollyAndDictionaryApi.Handlers;

namespace PollyAndDictionaryApi
{
    /// <summary>
    /// Web API routing config
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Web API routing register
        /// </summary>
        /// <param name="config">HttpConfiguration</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //// regist custom response entity
            config.MessageHandlers.Add(new CustomResponseHandler());
        }
    }
}