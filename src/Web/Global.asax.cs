using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Web.Mvc;
using Web.Infrastructure;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    [CoverageExcludeAttribute]
    public class TrackyApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("robots.txt");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Web.Controllers" }
            );
        }

        protected void Application_Start()
        {
            // Routing
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            // Dependency injection
            ControllerBuilder.Current.SetControllerFactory(new TrackyControllerFactory());

            // Auto-mapper
            TrackyMapping.SetupMapping();

            // RouteDebugger
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }
    }
}