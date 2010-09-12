<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MyTasks.aspx.cs" Inherits="trackyourtasks.net.MyTasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/tasksgrid2.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            var ser = new Services.TasksService();
            $('#tasks').tasksgrid(
                'newTaskName',
                'createTask',
                'submitData',
                loadData,
                submitData,
                deleteData
            );

            function loadData(callback) {
                return ser.GetAllTasks(callback, null, null);
            }

            function submitData(data, callback) {
                return ser.Submit(data, callback, null, null);
            }

            function deleteData(data, callback) {
                return ser.Delete(data, callback, null, null);
            }
        }
        );
           
    </script>
</asp:Content>
<asp:Content ID="MyTasksContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="Services/TasksService.svc" />
        </Services>
    </asp:ScriptManager>
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
