using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyt.Core.Tests.Framework;
using Trackyt.Core.DAL.Repositories.Impl;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Extensions;
using System.Data.Linq;

namespace Trackyt.Database.Tests.Tests.DAL
{
    [TestFixture]
    public class BlogPostsRepositoryTests
    {

        #region test data

        private static void SubmitTenBlogpostsToRepository(BlogPostsRepository repository)
        {
            for (var blog = 0; blog < 10; blog++)
            {
                var post = new BlogPost
                {
                    Url = "Url-" + blog,
                    Title = "Some new post: " + blog,
                    Body = "<p>This is new post in blog</p>",
                    CreatedBy = "AlexanderB",
                    CreatedDate = (DateTime.Now.AddDays(blog))
                };

                repository.Save(post);
            }
        }

        #endregion

        [Test]
        public void SaveBlogPost()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);
                var post = new BlogPost
                {
                    Url = "Some-new-post",
                    Title = "Some new post",
                    Body = "<p>This is new post in blog</p>",
                    CreatedBy = "AlexanderB",
                    CreatedDate = DateTime.Now
                };

                //act
                repository.Save(post);

                //assert
                Assert.That(post.Id, Is.GreaterThan(0));
            }
        }

        [Test]
        public void Save_UpdatePost()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);
                var post = new BlogPost
                {
                    Url = "Some-new-post",
                    Title = "Some new post",
                    Body = "<p>This is new post in blog</p>",
                    CreatedBy = "AlexanderB",
                    CreatedDate = DateTime.Now
                };

                repository.Save(post);

                var blogPost = repository.BlogPosts.Where(p => p.Url == post.Url).Single();
                
                // act
                blogPost.Title += "(updated)";
                repository.Save(blogPost);

                //assert
                var updatedPost = repository.BlogPosts.Where(p => p.Url == post.Url).Single();
                Assert.That(updatedPost.Title, Is.EqualTo("Some new post(updated)"));
            }
        }

        [Test]
        public void DeletePost()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);
                var post = new BlogPost
                {
                    Url = "Some-new-post",
                    Title = "Some new post",
                    Body = "<p>This is new post in blog</p>",
                    CreatedBy = "AlexanderB",
                    CreatedDate = DateTime.Now
                };

                repository.Save(post);

                //act
                repository.Delete(post);

                //assert
                var foundTask = repository.BlogPosts.WithId(post.Id);
                Assert.That(foundTask, Is.Null);
            }
        }

        [Test]
        public void PagingGetFirstPage()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);

                SubmitTenBlogpostsToRepository(repository);

                //act
                var page = repository.BlogPosts.Page(1, 5);

                //assert
                Assert.That(page, Is.Not.Null);
                Assert.That(page.Count(), Is.EqualTo(5));
                Assert.That(page.First().Url, Is.EqualTo("Url-9"));
            }
        }

        [Test]
        public void PagingGetSecondPage()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);

                SubmitTenBlogpostsToRepository(repository);

                //act
                var page = repository.BlogPosts.Page(2, 5);

                //assert
                Assert.That(page, Is.Not.Null);
                Assert.That(page.Count(), Is.EqualTo(5));
                Assert.That(page.First().Url, Is.EqualTo("Url-4"));
            }
        }

        [Test]
        public void PagingGetNonExistingPage()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);

                SubmitTenBlogpostsToRepository(repository);

                //act
                var page = repository.BlogPosts.Page(3, 5);

                //assert
                Assert.That(page, Is.Not.Null);
                Assert.That(page.Count(), Is.EqualTo(0));
            }
        }

        [Test]
        public void BlogPostsSortedByDate()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);

                SubmitTenBlogpostsToRepository(repository);

                //act
                var posts = repository.BlogPosts.ToArray();

                //post
                Assert.That(posts, Is.Not.Null);
                Assert.That(posts[0].CreatedDate, Is.GreaterThan(posts[1].CreatedDate));
                Assert.That(posts[1].CreatedDate, Is.GreaterThan(posts[2].CreatedDate));
                Assert.That(posts[2].CreatedDate, Is.GreaterThan(posts[3].CreatedDate));
            }
        }

        [Test]
        public void GetBlogPostByUrl()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);

                SubmitTenBlogpostsToRepository(repository);

                //act
                var post = repository.BlogPosts.WithUrl("Url-0");

                //assert
                Assert.That(post, Is.Not.Null);
                Assert.That(post.Url, Is.EqualTo("Url-0"));
            }
        }

        [Test]
        [ExpectedException(typeof(DuplicateKeyException))]
        public void SubmitBlogPostWithSameUrl()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);

                //act / post
                repository.Save(new BlogPost { Url = "1", Title = "1", Body = "b", CreatedDate = DateTime.Now, CreatedBy = "c"});
                repository.Save(new BlogPost { Url = "1", Title = "1", Body = "b", CreatedDate = DateTime.Now, CreatedBy = "c"});
            }

        }
    }
}
