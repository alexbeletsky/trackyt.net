<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/BlogManagement.Master" Inherits="System.Web.Mvc.ViewPage<Trackyt.Core.DAL.DataModel.BlogPost>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <span>Congratulations! New blog post has been <span class="green">successfully</span> updated. 
    <%: Html.ActionLink("View post", "PostByUrl", new { area = "Blog", controller = "Posts", url = Model.Url })%>
    </span>
    <div>
        <%: Html.ActionLink("Back to posts", "AllPosts") %>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
