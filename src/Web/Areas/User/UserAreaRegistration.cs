using System.Web.Mvc;

namespace Web.Areas.User
{
    [CoverageExcludeAttribute]
    public class UserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "user";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "User_dashboard",
                "user/{email}",
                new { action = "Index", controller = "Dashboard" },
                 new string[] { "Web.Areas.User.Controllers" });
        }
    }
}
