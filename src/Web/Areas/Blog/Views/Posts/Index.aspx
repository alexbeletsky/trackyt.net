<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Public/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage<IList<Trackyourtasks.Core.DAL.DataModel.BlogPost>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Blog content</h2>

    <% foreach (var blogPost in Model)
       { %>
            <div class="blogpost">
                <div class="header">
                    <div class="title">
                        <%: blogPost.Title %>
                    </div>
                    <div class="date">
                        Published: <%: blogPost.CreatedDate.ToShortDateString() %>
                    </div>
                </div>
                <div class="body">
                    <%: blogPost.Body %>
                </div>
                <div class="footer">
                </div>
            </div> 
    <% } %>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
