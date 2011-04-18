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
                "ApiV11_tasks_updateposition",
                "api/v1.1/{apiToken}/tasks/update/{taskId}/position",
                new { controller = "ApiV11", action = "UpdatePosition" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );

            context.MapRoute(
                "ApiV11_tasks_updatedescription",
                "api/v1.1/{apiToken}/tasks/update/{taskId}/description",
                new { controller = "ApiV11", action = "UpdateDescription" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );

            context.MapRoute(
                "ApiV11_tasks_updateplanneddate",
                "api/v1.1/{apiToken}/tasks/update/{taskId}/planneddate",
                new { controller = "ApiV11", action = "UpdatePlannedDate" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );


            context.MapRoute(
                "ApiV11_tasks_getall",
                "api/v1.1/{apiToken}/tasks/all",
                new { controller = "ApiV11", action = "All" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );

            context.MapRoute(
                "ApiV11_tasks_gettotat",
                "api/v1.1/{apiToken}/tasks/total",
                new { controller = "ApiV11", action = "Total" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );

            context.MapRoute(
                "ApiV11_tasks_add",
                "api/v1.1/{apiToken}/tasks/add",
                new { controller = "ApiV11", action = "Add" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );

            context.MapRoute(
                "ApiV11_tasks_putdone",
                "api/v1.1/{apiToken}/tasks/done",
                new { controller = "ApiV11", action = "Done" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );

            context.MapRoute(
                "ApiV11_tasks_getdone",
                "api/v1.1/{apiToken}/tasks/done/{taskId}",
                new { controller = "ApiV11", action = "Done" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );

            context.MapRoute(
                "ApiV11_tasks_totaldone",
                "api/v1.1/{apiToken}/tasks/totaldone",
                new { controller = "ApiV11", action = "TotalDone" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );

            context.MapRoute(
                "ApiV11_tasks_sharelink",
                "api/v1.1/{apiToken}/share/link",
                new { controller = "ApiV11", action = "ShareLink" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );

            context.MapRoute(
                "ApiV11_tasks_deletealldone",
                "api/v1.1/{apiToken}/tasks/delete/alldone",
                new { controller = "ApiV11", action = "DeleteAllDone" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );

            context.MapRoute(
                "ApiV11_tasks_undoall",
                "api/v1.1/{apiToken}/tasks/undo/all",
                new { controller = "ApiV11", action = "UndoAll" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );

            context.MapRoute(
                "ApiV11_tasks_undo",
                "api/v1.1/{apiToken}/tasks/undo/{taskId}",
                new { controller = "ApiV11", action = "Undo" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );

            //API v1 map
            context.MapRoute(
                "ApiV11_tasks",
                "api/v1.1/{apiToken}/tasks/{action}/{taskId}",
                new { controller = "ApiV11" },
                new { apiToken = @"[a-fA-F\d]{32}" },
                new string[] { "Web.API.v11.Controllers" }
            );
        }
    }
}