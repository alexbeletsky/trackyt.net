<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Tracky.net | Dashboard</title>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/Tracky/tracky.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/Tracky/trackycontroller.js") %>"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <%: Html.TextBox("userId", ViewData["UserId"], new { type = "hidden" }) %>
    <%: Html.TextBox("api", ViewData["Api"], new { type = "hidden" }) %>

    <div id="submit">
        <a id="submitData" href="#" class="submit">Submit</a>
    </div>
    <div class="center">
        <div id="tasks-container">
            <div id="add-task">
                <input id="newTaskName" type="text" />
                <input id="createTask" type="button" value="New task" />
            </div>
            <div style="clear: both;" />
            <div id="tasks">
            </div>
        </div>
    </div>
</asp:Content>
