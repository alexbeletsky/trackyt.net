<%@ Page Title="Trackyt.net | Dashboard" Language="C#" MasterPageFile="~/Areas/User/Views/Shared/Dashboard.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <%: Html.TextBox("apiToken", ViewData["ApiToken"], new { type = "hidden" }) %>
    <%: Html.TextBox("api", ViewData["Api"], new { type = "hidden" }) %>

    <div id="container">
        <div id="new-task-control">
            <div id="new-task-container">
                <input id="task-description" type="text" placeholder="New task description" />
                
                <div class="context" id="summary">
                    <div class="context-item" id="project-all"></div>                    
                    <div class="context-item" id="project-done"></div>
                </div>
                <div class="context right" id="actions">
                    <div class="context-item" id="action-delete-all"></div>                    
                    <div class="context-item" id="action-undo-all"></div>                    
                </div>
                <div class="clear"></div>
            </div>
            </div>  
        <div id="tasks">

        </div>
</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/Api/api.js") + "?ver=1013" %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/Controls/control.tasks.js") + "?ver=1013" %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/Controllers/controller.dashboard.js") + "?ver=1013" %>"></script>
</asp:Content>
