<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MyTasks.aspx.cs" Inherits="trackyourtasks.net.MyTasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/tasksgrid2.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            $('#tasks').tasksgrid(
                'newTaskName',
                'createTask'
            );
        }
        );
           
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
