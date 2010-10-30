<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Public/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage<Web.Areas.Blog.Models.BlogPosts>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Blog content</h2>

    <%: Html.Partial("Pager") %>

    <% foreach (var blogPost in Model.Content)
       { %>
            <div class="blogpost">
                <div class="header">
                    <div class="title">
                        <%: blogPost.Title %>
                    </div>
                    <div class="date">
                        Published on: <%: blogPost.CreatedDate.ToShortDateString() %>
                    </div>
                    <div class="createdby">
                        By: <%: blogPost.CreatedBy %>
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





</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
