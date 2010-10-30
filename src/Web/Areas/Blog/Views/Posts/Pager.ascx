<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Web.Areas.Blog.Models.BlogPosts>" %>
<%@ Import Namespace="Web.Helpers.Extensions" %>

<%: Html.Pager(Url.Action("Page", "Posts", new { area = "Blog", id="{0}"}), Model.TotalPages, Model.CurrentPage)%>