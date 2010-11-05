using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Admin.Models
{
    public class AdminBlogSummary
    {
        public AdminBlogSummary(int totalPosts)
        {
            TotalPosts = totalPosts;
        }

        public int TotalPosts { get; private set; }
    }
}