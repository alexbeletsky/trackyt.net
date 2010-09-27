<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Tracky.net | Dashboard</title>
    <script type="text/javascript" src="/Scripts/Tracky/tasksgrid2.js"></script>
    <script type="text/javascript">
        $().ready(function () {
            $('#tasks').tasksgrid(
                'newTaskName',
                'createTask',
                'submitData',
                loadData,
                submitData,
                deleteData
            );

            function loadData(callback) {
                $.post('/API/v1/GetAllTasks/' + $('#userId').val(), null, callback, 'json');
            }

            function submitData(data, callback) {
                $.postJson('/API/v1/Submit', data, callback);
            }

            function deleteData(data, callback) {
                $.postJson('/API/v1/Delete', data, callback);
            }
        }
        );
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <%: Html.TextBox("userId", ViewData["UserId"], new { type = "hidden" }) %>

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
