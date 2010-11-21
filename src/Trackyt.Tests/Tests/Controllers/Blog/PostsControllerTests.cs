using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Areas.Blog.Controllers;
using Moq;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.DAL.DataModel;
using System.Web.Mvc;
using Web.Areas.Blog.Models;

namespace Trackyt.Core.Tests.Tests.Controllers.Blog
{
    [TestFixture]
    public class PostsControllerTests
    {
        #region test data

        private static Mock<IBlogPostsRepository> CreateRepositoryMock(int blogPostsCount = 10)
        {
            var blogRepositoryMock = new Mock<IBlogPostsRepository>();

            var posts = new List<BlogPost>();
            for (var blog = 0; blog < blogPostsCount; blog++)
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
            var model = result.ViewData.Model as BlogPosts;
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
            var model = result.ViewData.Model as BlogPosts;
            Assert.That(model.Content.Count, Is.EqualTo(5));
        }

        [Test]
        public void GetIndexReturnsFirstPage()
        {
            //arrange
            var blogRepositoryMock = CreateRepositoryMock();
            var controller = new PostsController(blogRepositoryMock.Object);

            //act
            var result = controller.Index() as ViewResult;

            //assert
            var model = result.ViewData.Model as BlogPosts;
            Assert.That(model.CurrentPage, Is.EqualTo(1));
        }

        [Test]
        public void GetIndexTotalPagesIsCorreclyCalculated_TenPosts_MakesTwoPages()
        {
            //arrange
            var blogRepositoryMock = CreateRepositoryMock();
            var controller = new PostsController(blogRepositoryMock.Object);

            //act
            var result = controller.Index() as ViewResult;

            //assert
            var model = result.ViewData.Model as BlogPosts;
            Assert.That(model.TotalPages, Is.EqualTo(2));
        }

        [Test]
        public void GetIndexTotalPagesIsCorreclyCalculated_FivePosts_MakesOnePage()
        {
            //arrange
            var blogRepositoryMock = CreateRepositoryMock(5);
            var controller = new PostsController(blogRepositoryMock.Object);

            //act
            var result = controller.Index() as ViewResult;

            //assert
            var model = result.ViewData.Model as BlogPosts;
            Assert.That(model.TotalPages, Is.EqualTo(1));
        }

        [Test]
        public void GetIndexTotalPagesIsCorreclyCalculated_OnePost_MakesOnePage()
        {
            //arrange
            var blogRepositoryMock = CreateRepositoryMock(1);
            var controller = new PostsController(blogRepositoryMock.Object);

            //act
            var result = controller.Index() as ViewResult;

            //assert
            var model = result.ViewData.Model as BlogPosts;
            Assert.That(model.TotalPages, Is.EqualTo(1));
        }


        [Test]
        public void GetIndexTotalPagesIsCorreclyCalculated_EightPost_MakesTwoPage()
        {
            //arrange
            var blogRepositoryMock = CreateRepositoryMock(8);
            var controller = new PostsController(blogRepositoryMock.Object);

            //act
            var result = controller.Index() as ViewResult;

            //assert
            var model = result.ViewData.Model as BlogPosts;
            Assert.That(model.TotalPages, Is.EqualTo(2));
        }


        [Test]
        public void GetIndexTotalPagesIsCorreclyCalculated_ElevenPost_MakesThreePage()
        {
            //arrange
            var blogRepositoryMock = CreateRepositoryMock(11);
            var controller = new PostsController(blogRepositoryMock.Object);

            //act
            var result = controller.Index() as ViewResult;

            //assert
            var model = result.ViewData.Model as BlogPosts;
            Assert.That(model.TotalPages, Is.EqualTo(3));
        }

        [Test]
        public void GetPages_ViewIsIndex()
        {
            //arrange
            var blogRepositoryMock = CreateRepositoryMock(10);
            var controller = new PostsController(blogRepositoryMock.Object);

            //act
            var result = controller.Page(1) as ViewResult;

            //assert
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void GetPages_First_ReturnFivePosts()
        {
            //arrange
            var blogRepositoryMock = CreateRepositoryMock(10);
            var controller = new PostsController(blogRepositoryMock.Object);

            //act
            var result = controller.Page(1) as ViewResult;

            //assert
            var model = result.ViewData.Model as BlogPosts;
            Assert.That(model.CurrentPage, Is.EqualTo(1));
            Assert.That(model.Content.Count, Is.EqualTo(5));
        }

        [Test]
        public void GetPages_Second_ReturnsFivePosts()
        {
            //arrange
            var blogRepositoryMock = CreateRepositoryMock(10);
            var controller = new PostsController(blogRepositoryMock.Object);

            //act
            var result = controller.Page(2) as ViewResult;

            //assert
            var model = result.ViewData.Model as BlogPosts;
            Assert.That(model.CurrentPage, Is.EqualTo(2));
            Assert.That(model.Content.Count, Is.EqualTo(5));
        }

        [Test]
        public void GetPages_Third_ReturnsZeroPosts()
        {
            //arrange
            var blogRepositoryMock = CreateRepositoryMock(10);
            var controller = new PostsController(blogRepositoryMock.Object);

            //act
            var result = controller.Page(3) as ViewResult;

            //assert
            var model = result.ViewData.Model as BlogPosts;
            Assert.That(model.CurrentPage, Is.EqualTo(3));
            Assert.That(model.Content.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetPostByUrl_ReturnsBlogPost()
        {
            //arrange
            var blogRepositoryMock = CreateRepositoryMock(10);
            var controller = new PostsController(blogRepositoryMock.Object);

            //act
            var result = controller.PostByUrl("Url-0") as ViewResult;

            //assert
            var model = result.ViewData.Model as BlogPost;
            Assert.That(model, Is.Not.Null);
        }
    }
}
