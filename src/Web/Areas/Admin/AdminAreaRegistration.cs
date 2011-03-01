using System.Web.Mvc;

namespace Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Post_Management",
                "admin/blogmanagement/post/{action}/{url}",
                new { action = "Index", controller = "BlogManagement" },
                new string[] { "Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_admin",
                "admin",
                new { action = "Index", controller = "Login" },
                new string[] { "Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", controller = "Dashboard", id = UrlParameter.Optional },
                new string[] { "Web.Areas.Admin.Controllers" }

            );
        }
    }
}
