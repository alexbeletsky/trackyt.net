using System.Collections.Generic;
using Trackyt.Core.DAL.DataModel;

namespace Web.Areas.Blog.Models
{
    public class BlogPosts
    {
        public BlogPosts(int currentPage, int totalPages, IList<BlogPost> content)
        {
            CurrentPage = currentPage;
            Content = content;
            TotalPages = totalPages;
        }

        public int TotalPages { get; private set; }
        public int CurrentPage { get; private set; }
        public IList<BlogPost> Content { get; private set; }
    }
}