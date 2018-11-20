using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace PollyAndDictionaryApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutofacConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterFilter(GlobalFilters.Filters);
            ViewEnginesConfig.RegisterViewEngines(ViewEngines.Engines);

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }
    }
}
