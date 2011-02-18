<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/TaskManagement.Master" Inherits="System.Web.Mvc.ViewPage<Web.Areas.Admin.Models.TaskSummaryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="summary-container">
        <span>
            We currently have <span class="green"><%: Model.TotalTasks %></span> created tasks. Users totally logged <span class="green"><%: Model.TotalLoggedTime %> seconds</span>.
        </span>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
