using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyourtasks.Core.DAL.Extensions;
using Trackyourtasks.Core.DAL.Repositories;

namespace Web.Areas.Blog.Controllers
{
    public class PostsController : Controller
    {
        private static readonly int PageSize = 5;

        private IBlogPostsRepository _repository;

        public PostsController(IBlogPostsRepository postsRepository)
        {
            _repository = postsRepository;
        }

        //
        // GET: /Blog/Blog/

        public ActionResult Index()
        {
            var posts = _repository.BlogPosts.Page(0, PageSize);

            return View(posts.ToList());
        }

    }
}
