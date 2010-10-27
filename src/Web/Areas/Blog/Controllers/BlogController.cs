using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyourtasks.Core.DAL.Repositories.Impl;
using Trackyourtasks.Core.DAL.Extensions;
using Web.Areas.Blog.Models;

namespace Web.Areas.Blog.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/Blog/

        public ActionResult Index()
        {
            //TODO: use dependency injection 
            //TODO: delete not required using
            //TODO: tests
            var repositoty = new BlogPostsRepository();
            var page = new Page(0, repositoty.BlogPosts.Page(0, 5).ToList());

            return View(page);
        }

    }
}
