using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyt.Core.DAL.Repositories;

namespace Web.Areas.Blog.Controllers
{
    //http://stackoverflow.com/questions/11915/rss-feeds-in-asp-net-mvc

    public class RssController : Controller
    {
        private IBlogPostsRepository _postsReposiotory;

        public RssController(IBlogPostsRepository postsRepository)
        {
            _postsReposiotory = postsRepository;
        }

        public ActionResult Feed()
        {
            var postsForFeed = _postsReposiotory.BlogPosts.Take(10);
            return View(postsForFeed.ToList());
        }
    }
}
