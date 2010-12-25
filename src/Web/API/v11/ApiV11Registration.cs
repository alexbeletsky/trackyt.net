using System.Web.Mvc;

namespace Web.API.v11
{
    public class ApiV1Registration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "ApiV11"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //API v1 map
            context.MapRoute(
                "ApiV11",
                "API/v1.1/{action}/{apiToken}",
                new { controller = "ApiV11", apiToken = UrlParameter.Optional }
            );
        }
    }
}