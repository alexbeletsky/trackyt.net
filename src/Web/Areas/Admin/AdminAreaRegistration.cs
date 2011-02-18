using System.Web.Mvc;

namespace Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Post_Management",
                "Admin/BlogManagement/post/{action}/{url}",
                new { action = "Index", controller = "BlogManagement" },
                new string[] { "Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_admin",
                "Admin",
                new { action = "Index", controller = "Login" },
                new string[] { "Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", controller = "Dashboard", id = UrlParameter.Optional },
                new string[] { "Web.Areas.Admin.Controllers" }

            );
        }
    }
}
