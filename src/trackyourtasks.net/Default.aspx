<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="trackyourtasks.net._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script src="Scripts/tasksgrid.js" type="text/javascript"></script>
   
    <script type="text/javascript">
        $().ready(function () {
            var ser = new Services.TasksService();
            var table = $("#tasks").tasksgrid(loadData, submitData);

            function loadData(callback) {
                return ser.GetAllTasks(callback, null, null);
            }

            function submitData(data, callback) {
                return ser.Submit(data, callback, null, null);
            }
        }
      );
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <services>
            <asp:ServiceReference Path="Services/TasksService.svc" />
        </services>
    </asp:ScriptManager>

    <table id="tasks">
        <thead>
        </thead>
        <tbody>
        </tbody>
    </table>

<%--    <select id="1">
        <option label="xx" value="1" selected="false"></option>
        <option label="xxx" value="1" selected="false"></option>
        <option label="xxxx" value="1" selected="true"></option>
    </select>
--%>
</asp:Content>
