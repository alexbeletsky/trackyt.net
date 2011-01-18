using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using Trackyt.Core.DAL.Repositories;
using Web.Controllers;
using System.Web.Mvc;
using Trackyt.Core.DAL.DataModel;
using Web.Models;

namespace Trackyt.Core.Tests.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void Smoke()
        {
            // arrange
            var posts = new Mock<IBlogPostsRepository>();
            var controller = new HomeController(posts.Object);

            // act / assert 
            Assert.That(controller, Is.Not.Null);
        }

        [Test]
        public void LastArticleFromBlog_IfNoPostsInRepo_ReturnEmptyView()
        {
            // arrange
            var posts = new Mock<IBlogPostsRepository>();
            var controller = new HomeController(posts.Object);

            // act
            var result = controller.LastArticleFromBlog() as ViewResult;

            // assert
            Assert.That(result.ViewData.Model, Is.Null);
        }

        [Test]
        public void LastArticleFromBlog_ThereArePostsInRepo_ReturnsLastPost()
        {
            // arrange
            var posts = new Mock<IBlogPostsRepository>();
            var controller = new HomeController(posts.Object);

            var postsInRepo = CreatePostsList();
            posts.Setup(p => p.BlogPosts).Returns(postsInRepo.AsQueryable());

            // act
            var result = controller.LastArticleFromBlog() as ViewResult;

            // assert
            var post = result.ViewData.Model as LastArticleFromBlogModel;
            Assert.That(post, Is.Not.Null);
            Assert.That(post.Title, Is.EqualTo("Second"));
        }

        private IList<BlogPost> CreatePostsList()
        {
            var currentDate = DateTime.Now;

            return new List<BlogPost>
            {
                new BlogPost { Url = "2", Title = "Second", Body = "body", CreatedDate = currentDate },
                new BlogPost { Url = "1", Title = "First", Body = "body", CreatedDate = currentDate.AddDays(-1) }
            };
        }
    }
}
