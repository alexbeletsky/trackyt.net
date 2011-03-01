using System.Web.Mvc;

namespace Web.API.v11
{
    public class ApiV1Registration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "apiV11"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //API v1 map
            context.MapRoute(
                "ApiV11_auth",
                "api/v1.1/authenticate",
                new { controller = "ApiV11", action = "Authenticate" },
                new string[] { "Web.API.v11.Controllers" }
            );

            context.MapRoute(
                "ApiV11_tasks_startall",
                "api/v1.1/{apiToken}/tasks/start/all",
                new { controller = "ApiV11", action = "StartAll" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );

            context.MapRoute(
                "ApiV11_tasks_stopall",
                "api/v1.1/{apiToken}/tasks/stop/all",
                new { controller = "ApiV11", action = "StopAll" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );

            //API v1 map
            context.MapRoute(
                "ApiV11_tasks",
                "api/v1.1/{apiToken}/tasks/{action}/{taskId}",
                new { controller = "ApiV11", taskId = UrlParameter.Optional },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );
        }
    }
}