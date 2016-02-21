using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Web.Mvc;
using System.Web.Routing;
using Owin;

namespace Data.gov.ua
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //var config = new HttpSelfHostConfiguration("http://localhost:50887");

            //HttpSelfHostServer server = new HttpSelfHostServer(config);

            //server.OpenAsync().Wait();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
