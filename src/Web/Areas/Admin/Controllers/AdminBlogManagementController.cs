﻿using System;
using System.Linq;
using System.Web.Mvc;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Repositories;
using Web.Areas.Admin.Models;
using Web.Infrastructure.Security;

namespace Web.Areas.Admin.Controllers
{
    [TrackyAuthorizeAttribute(Users = "Admin", LoginArea = "Admin", LoginController = "AdminLogin")]
    public class AdminBlogManagementController : Controller
    {
        private IBlogPostsRepository _blogRepository;

        public AdminBlogManagementController(IBlogPostsRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Summary()
        {
            var blogPostCount = _blogRepository.BlogPosts.Count();

            return View(new AdminBlogSummary(blogPostCount));
        }

        public ActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPost(BlogPost post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    post.Url = CreatePostUrl(post.Title);                    
                    post.CreatedDate = DateTime.Now; 

                    _blogRepository.SaveBlogPost(post);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Post could not be added. " + e.Message);
            }

            return View(post);
        }

        private string CreatePostUrl(string title)
        {
            var titleWithoutPunctuation = new string(title.Where(c => !char.IsPunctuation(c)).ToArray());
            return titleWithoutPunctuation.ToLower().Trim().Replace(" ", "-");
        }

    }
}
