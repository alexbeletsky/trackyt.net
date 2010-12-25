using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.DAL.Repositories
{
    /// <summary>
    /// Repository of Blog posts
    /// </summary>
    public interface IBlogPostsRepository
    {
        /// <summary>
        /// Gets all blog posts from repository
        /// </summary>
        IQueryable<BlogPost> BlogPosts { get; }

        /// <summary>
        /// Creates new record and initialize Id for new objects and update exiting objects
        /// </summary>
        /// <param name="blogPost">Blog post object</param>
        void Save(BlogPost blogPost);

        /// <summary>
        /// Deletes blog post
        /// </summary>
        /// <param name="blogPost"></param>
        void Delete(BlogPost blogPost);
    }
}
