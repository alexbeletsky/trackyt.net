using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Areas.Blog.Controllers;
using Moq;
using Trackyourtasks.Core.DAL.Repositories;
using Trackyourtasks.Core.DAL.DataModel;
using System.Web.Mvc;

namespace Trackyourtasks.Core.Tests.Tests.Controllers.Blog
{
    [TestFixture]
    public class PostsControllerTests
    {
        #region test data

        private static Mock<IBlogPostsRepository> CreateRepositoryMock()
        {
            var blogRepositoryMock = new Mock<IBlogPostsRepository>();

            var posts = new List<BlogPost>();
            for (var blog = 0; blog < 10; blog++)
            {
                var post = new BlogPost
                {
                    Url = "Url-" + blog,
                    Title = "Some new post: " + blog,
                    Body = "<p>This is new post in blog</p>",
                    CreatedDate = (DateTime.Now.AddDays(blog))
                };

                posts.Add(post);
            }

            blogRepositoryMock.Setup(b => b.BlogPosts).Returns(posts.AsQueryable());

            return blogRepositoryMock;
        }

        #endregion

        [Test]
        public void Smoke()
        {
            //arrange
            var blogRepositoryMock = CreateRepositoryMock();
            var controller = new PostsController(blogRepositoryMock.Object);

            //act / assert
            Assert.That(controller, Is.Not.Null);
        }

        [Test]
        public void GetIndexReturnBlogPage()
        {
            //arrange
            var blogRepositoryMock = CreateRepositoryMock();
            var controller = new PostsController(blogRepositoryMock.Object);

            //act
            var result = controller.Index() as ViewResult;

            //assert
            var model = result.ViewData.Model as IList<BlogPost>;
            Assert.That(model, Is.Not.Null);
        }

        [Test]
        public void GetIndexPageSizeIsFive()
        {
            //arrange
            var blogRepositoryMock = CreateRepositoryMock();
            var controller = new PostsController(blogRepositoryMock.Object);

            //act
            var result = controller.Index() as ViewResult;

            //assert
            var model = result.ViewData.Model as IList<BlogPost>;
            Assert.That(model.Count, Is.EqualTo(5));
        }
    }
}
