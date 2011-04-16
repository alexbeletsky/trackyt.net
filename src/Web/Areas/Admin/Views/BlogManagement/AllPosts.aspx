<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/BlogManagement.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace = "Web.Helpers.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    Search: <input id="post-name" />
    <div id="edit-posts-table"></div>
    <div id="edit-posts-pager"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%: Url.ContentWithVersion("~/Scripts/Controls/control.editposts.js") %>" type="text/javascript"></script>
    <script src="<%: Url.ContentWithVersion("~/Scripts/Controllers/controller.editposts.js") %>" type="text/javascript"></script>
</asp:Content>
