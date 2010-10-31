using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyourtasks.Core.DAL.Extensions;
using Trackyourtasks.Core.DAL.Repositories;
using Web.Areas.Blog.Models;
using Web.Helpers.Extensions;

namespace Web.Areas.Blog.Controllers
{
    public class PostsController : Controller
    {
        private static readonly int PostsOnPageCount = 5;

        private IBlogPostsRepository _repository;
        private int _totalPosts;

        public PostsController(IBlogPostsRepository postsRepository)
        {
            _repository = postsRepository;
            _totalPosts = _repository.BlogPosts.Count();
        }

        public ActionResult Index()
        {
            var model = GetPostsForPage(1);

            return View(model);
        }

        public ActionResult Page(int id)
        {
            var model = GetPostsForPage(id);

            return View("Index", model);
        }

        private BlogPosts GetPostsForPage(int currentPage)
        {
            var posts = _repository.BlogPosts.Page(currentPage, PostsOnPageCount);
            var model = new BlogPosts(currentPage, TotalPages(), posts.ToList());
            return model;
        }

        private int TotalPages()
        {
            var pages =  _totalPosts / PostsOnPageCount;
            if (pages == 0)
            {
                pages = 1;
            }

            return pages;
        }

    }
}
