<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Public/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <div id="screenshot">
        <div id="slogan">
            Track Your Tasks on .NET
        </div>
        <div id="info">
            <p>
                <strong>trackyt.net</strong> is web application that makes you time management easier. 
            </p>
            <p>Sign in right now and improve your performance!</p>
            <div class="green-button bottom">
                <%: Html.ActionLink("Sign up for free", "Index", new { area = "Public", controller = "Registration" })%>
                <p>
                    No registration needed
                </p>
            </div>
        </div>
        
    </div>
    <div class="content">
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
