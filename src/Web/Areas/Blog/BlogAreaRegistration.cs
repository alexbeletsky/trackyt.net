using System.Web.Mvc;

namespace Web.Areas.Blog
{
    public class BlogAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "blog";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Blog_RssFeed",
                "blog/feed.rss",
                new { controller = "Rss", action = "Feed" },
                new string[] { "Web.Areas.Blog.Controllers" }
            );

            context.MapRoute(
                "Blog_PostByUrl",
                "blog/posts/{url}",
                new { controller = "Posts", action = "PostByUrl" },
                new string[] { "Web.Areas.Blog.Controllers" }
            );

            context.MapRoute(
                "Blog_default",
                "blog/{controller}/{action}/{id}",
                new { action = "Index", controller = "Posts", id = UrlParameter.Optional },
                new string[] { "Web.Areas.Blog.Controllers" }
            );

        }
    }
}
