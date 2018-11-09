using NSwag.AspNet.Owin;
using System.Web.Http;
using System.Web.Routing;

namespace Baufest.NHibernate.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            RouteTable.Routes.MapOwinPath("swagger", app =>
            {
                app.UseSwaggerUi3(typeof(WebApiApplication).Assembly, settings =>
                {
                    settings.MiddlewareBasePath = "/swagger";
                    settings.GeneratorSettings.Title = "NHibernate";
                });
            });
        }
    }
}
