<%@ Page Title="Trackyt.net | Dashboard" Language="C#" MasterPageFile="~/Areas/User/Views/Shared/Dashboard.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <%: Html.TextBox("apiToken", ViewData["ApiToken"], new { type = "hidden" }) %>
    <%: Html.TextBox("api", ViewData["Api"], new { type = "hidden" }) %>

    <div id="container">
        <div id="new-task-control">
            <div id="new-task-container">
                <input id="task-description" type="text" />
                <input id="add-task" type="button" value="Add task" />
            </div>
            <div id="operation-controls">
                <input id="start-all" type="button" value="Start All"/>
                <span>|</span>
                <input id="stop-all" type="button" value="Stop All"/>
            </div>        
        </div>  
        <div id="tasks">
        </div>
</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/Tracky/api.js") + "?ver=1011" %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/Tracky/control.tasks.js") + "?ver=1011" %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/Tracky/controller.tasks.js") + "?ver=1011" %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/Tracky/controller.account.js") + "?ver=1011" %>"></script>
</asp:Content>
