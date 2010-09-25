<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" Inherits="System.Web.Mvc.ViewPage<Web.Areas.Public.Models.LoginModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Tracky.net | Login</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm(new { action = "Login" }))
       {%>
        <%: Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.")%>
        <p>
        Email:
        <%: Html.TextBoxFor(m => m.Email)%>        
        </p>
        <p>
        Password:
        <%: Html.TextBoxFor(m => m.Password, new { type = "password" })%>
        </p>
        <input type="submit" value="Login" />
    <% } %>
</asp:Content>
