<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" Inherits="System.Web.Mvc.ViewPage<Web.Areas.Public.Models.RegisterUserModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Tracky.net | Register new user</title>
    <script type="text/javascript">
        $().ready(function () {
            alert('ready');
            $("#RegistrationForm").submit(submit);
        }
        );

        function submit() {
            if ($('#Password').val() != $('#ConfirmPassword').val()) {
                $('#PasswordValidationMessage').html('Confirmed password does not match. Please correct');
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="registration">
        <% using (Html.BeginForm("Register", "Registration", FormMethod.Post, new { id="RegistrationForm" })) { %>
            <div id="PasswordValidationMessage" class="valiation"></div>
            <%: Html.ValidationSummary() %>
            <p>
            Email: <%: Html.TextBoxFor(m => m.Email) %>
            </p>
            <p>
            Password: <%: Html.TextBoxFor(m => m.Password, new { type = "password" })%>
            </p>
            <p>
            Confirm password: <%: Html.TextBoxFor(m => m.ConfirmPassword, new { type = "password" })%>
            </p>
            <input type="submit" value="Register"/>
        <%} %>
    </div>
</asp:Content>

