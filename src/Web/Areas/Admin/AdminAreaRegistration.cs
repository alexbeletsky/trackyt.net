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
                "Admin/AdminBlogManagement/post/{action}/{url}",
                new { action = "Index", controller = "AdminBlogManagement" }
            );

            
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", controller="AdminLogin", id = UrlParameter.Optional }
            );
        }
    }
}
