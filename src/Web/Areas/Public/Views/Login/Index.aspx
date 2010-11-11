<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Web.Areas.Public.Models.LoginModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tracky.net | Login</title>
    <link href="../../../../Content/public-login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="login-container">
        <div id="content">
        <h1>Tracky.net login</h1>
        <% using (Html.BeginForm("Login", "Login", FormMethod.Post, new { id = "login_form" }))
           {%>
        <%: Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.")%>
        <fieldset>
            <div id="email-label">
                <label for="email">
                    Email</label>
            </div>
            <div id="email-field">
                <%: Html.TextBoxFor(m => m.Email)%>
            </div>
            <div id="password-label">
                <label for="password">
                    Password</label>
            </div>
            <div id="password-field">
                <%: Html.TextBoxFor(m => m.Password, new { type = "password" })%>
            </div>
            <p>
                <input id="submit-button" type="submit" value="Login »" />
            </p>
        </fieldset>
        <% } %>
        </div>
    </div>
</body>
</html>
