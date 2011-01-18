<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Web.Models.LastArticleFromBlogModel>" %>
<% if (Model != null)
   { %>

<div id="last-article">
    <h3>
        Latest in blog: <%: Model.Title%></h3>
    <div>
        <p>
            <%: MvcHtmlString.Create(Model.HtmlBody)%>
        </p>
    </div>
</div>

<%} %>
