using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.DAL.Repositories.Impl
{
    public class BlogPostsRepository : IBlogPostsRepository
    {
        private TrackyDataContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public BlogPostsRepository() : this(new TrackyDataContext())
        {

        }

        /// <summary>
        /// Constructor used in unit tests
        /// </summary>
        /// <param name="context">Context</param>
        public BlogPostsRepository(TrackyDataContext context)
        {
            _context = context;
        }

        public IQueryable<BlogPost> BlogPosts
        {
            get 
            {
                return _context.BlogPosts;
            }
        }

        public void SaveBlogPost(BlogPost blogPost)
        {
            if (blogPost.Id == 0)
            {
                _context.BlogPosts.InsertOnSubmit(blogPost);
            }
            _context.SubmitChanges();
        }

        public void DeleteBlogPost(DataModel.BlogPost blogPost)
        {
            _context.BlogPosts.DeleteOnSubmit(blogPost);
            _context.SubmitChanges();
        }
    }
}
