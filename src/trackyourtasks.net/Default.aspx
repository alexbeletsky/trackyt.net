<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="trackyourtasks.net._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="Scripts/tasksgrid.js" type="text/javascript"></script>    
    <script type="text/javascript">
      $().ready(function() {
            $("#tasks").tasksgrid();
        }
      );
    </script>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table id="tasks"></table>
</asp:Content>
