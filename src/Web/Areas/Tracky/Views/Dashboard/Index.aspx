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
                $.post('/Tasks/GetAllTasks', null, callback, 'json');
            }

            function submitData(data, callback) {
                $.postJson('/Tasks/Submit', data, callback);
            }

            function deleteData(data, callback) {
                $.postJson('/Tasks/Delete', data, callback);
            }
        }
        );
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
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
