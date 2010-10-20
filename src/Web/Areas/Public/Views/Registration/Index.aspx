<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Public/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <h2>Registration</h2>
    <div id="registration">
        <p>
        Dear guest, you have several options of Tracky.net usage. You can either proceed to <%: Html.ActionLink("registration", "Register") %> page,
        or start to use service <%: Html.ActionLink("immediately", "QuickStart") %>. 
        </p>
        <p>
        <strong>Please note:</strong> if you are starting up without registration, we create a temporary user for you but all your data will be kept
        no longer than 30 days. So, you could try and decide to register any time you want. We will kindly notify you, as soon as you temporary account 
        is finishing.
        </p>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Tracky | Registration</title>
</asp:Content>
