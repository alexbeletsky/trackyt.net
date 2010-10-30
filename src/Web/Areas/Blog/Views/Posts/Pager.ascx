<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Web.Areas.Blog.Models.BlogPosts>" %>
<%@ Import Namespace="Web.Helpers.Extensions" %>

<%: Html.Pager(Model.TotalPages, Model.CurrentPage) %>