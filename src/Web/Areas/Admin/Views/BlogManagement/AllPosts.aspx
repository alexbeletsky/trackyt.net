<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/BlogManagement.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    Search: <input id="post-name" />
    <div id="edit-posts-table"></div>
    <div id="edit-posts-pager"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/Admin/control.editposts.js") + "?ver=106"%>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/Admin/controller.editposts.js") + "?ver=106"%>" type="text/javascript"></script>
</asp:Content>
