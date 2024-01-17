using FSite.Models.Binders;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace FSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ModelBinders.Binders.Add(typeof(DateTime), new MyDateTimeModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new MyDateTimeModelBinder());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //ModelBinders.Binders.Add(typeof(DateTime), new CustomDateModelBinder());
            //ModelBinders.Binders.Add(typeof(DateTime?), new CustomDateModelBinder());
        }
    }
}
