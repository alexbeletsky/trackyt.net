<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/UserManagement.Master" Inherits="System.Web.Mvc.ViewPage<Web.Areas.Admin.Models.AdminUserSummary>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="summary-container">
        <span>
            We currently have <span class="green"><%: Model.TotalRegisteredUsers %></span> total registered users!
            <span class="red"><%: Model.TempUsers %></span> of them are with temporal registration.
        </span>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
