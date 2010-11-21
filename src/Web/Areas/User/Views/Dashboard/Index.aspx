<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/User/Views/Shared/Dashboard.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <%: Html.TextBox("userId", ViewData["UserId"], new { type = "hidden" }) %>
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
                <span>|</span>                
                <input id="submit" type="button" value="Submit"/>
            </div>        
        </div>
        <div id="tasks">
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Trackyt.net | Dashboard</title>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/Tracky/tracky.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/Tracky/tracky-controller.js") %>"></script>
</asp:Content>
