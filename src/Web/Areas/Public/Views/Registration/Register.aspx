<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" Inherits="System.Web.Mvc.ViewPage<Web.Areas.Public.Models.RegisterUserModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="registration">
        <% using (Html.BeginForm()) { %>
            <p>
            Email: <%: Html.TextBoxFor(m => m.Email) %>
            </p>
            <p>
            Password: <%: Html.TextBoxFor(m => m.Password) %>
            </p>
            <p>
            Confirm password: <%: Html.TextBoxFor(m => m.ConfirmPassword) %>
            </p>
            <input type="submit" value="Register" />
        <%} %>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Tracky.net | Register new user</title>
</asp:Content>
