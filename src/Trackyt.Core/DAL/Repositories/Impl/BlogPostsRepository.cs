using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Extensions;
using System.Data.Linq;
using System.Configuration;

namespace Trackyt.Core.DAL.Repositories.Impl
{
    public class BlogPostsRepository : IBlogPostsRepository
    {
        private TrackytDataContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public BlogPostsRepository() : this(
            new TrackytDataContext(ConfigurationManager.ConnectionStrings["tracytdb"].ConnectionString))
        {

        }

        /// <summary>
        /// Constructor used in unit tests
        /// </summary>
        /// <param name="context">Context</param>
        public BlogPostsRepository(TrackytDataContext context)
        {
            _context = context;
        }

        public IQueryable<BlogPost> BlogPosts
        {
            get 
            {
                return _context.BlogPosts.OrderByDescending(p => p.CreatedDate);
            }
        }

        public void Save(BlogPost blogPost)
        {
            if (blogPost.Id == 0 && _context.BlogPosts.WithUrl(blogPost.Url) != null)
            {
                throw new DuplicateKeyException(blogPost, "Blog post with the same URL already exists. Please correct blog post title.");
            }

            if (blogPost.Id == 0)
            {
                _context.BlogPosts.InsertOnSubmit(blogPost);
            }
            _context.SubmitChanges();
        }

        public void Delete(DataModel.BlogPost blogPost)
        {
            _context.BlogPosts.DeleteOnSubmit(blogPost);
            _context.SubmitChanges();
        }
    }
}
