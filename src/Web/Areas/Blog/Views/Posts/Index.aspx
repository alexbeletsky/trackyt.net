<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Public/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage<Web.Areas.Blog.Models.BlogPosts>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div id="blogpost-container">

            <% if (Model.CurrentPage != 1)
           {
               %>
               <%: Html.Partial("Pager") %>
               <%
           } 
             %>

            <% foreach (var blogPost in Model.Content)
               { %>
                    <div class="blogpost">
                        <div class="header">
                            <div class="title">
                                <%: Html.ActionLink(blogPost.Title, "PostByUrl", new { url = blogPost.Url }) %>
                            </div>
                            <div class="posted-on">
                                Published on: <%: blogPost.CreatedDate.ToShortDateString() %> By: <%: blogPost.CreatedBy %>
                            </div>
                        </div>
                        <div class="body">
                            <%: MvcHtmlString.Create(blogPost.Body) %>
                        </div>
                        <div class="footer">
                        </div>
                    </div> 
            <% } %>

            <%: Html.Partial("Pager") %>

        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
