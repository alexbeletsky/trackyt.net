<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/BlogManagement.Master" Inherits="System.Web.Mvc.ViewPage<Web.Areas.Admin.Models.BlogSummaryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="summary-container">
        <span>
            We currently have <span class="green"><%: Model.TotalPosts %></span> total blog posts!
        </span>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
