using System.Web.Mvc;

namespace Web.API.v1
{
    public class ApiV1Registration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "API"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //API v1 map
            context.MapRoute(
                "ApiV1",
                "API/v1/{action}/{apiToken}",
                new { controller = "ApiV1" }
            );
        }
    }
}