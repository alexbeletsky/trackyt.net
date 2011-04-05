<%@ Page Title="Trackyt.net | Sign Up" Language="C#" Inherits="System.Web.Mvc.ViewPage<Web.Models.RegisterUserModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../Content/public-login.css?ver=1013" rel="stylesheet" type="text/css" />
    <script src="<%: Url.Content("~/Scripts/jquery-1.4.1.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/Controllers/controller.registration.js") + "?ver=1013" %>" type="text/javascript"></script>

    <link rel="shortcut icon" href="~/Content/Images/favicon.ico" type="image/x-icon" />
    <link rel="icon" href="~/Content/Images/favicon.ico" type="image/ico" />
</head>
<body>
    <div id="container">
        <div id="content">
            <h1 class="left">Sign Up</h1>
            <span id="else" class="left"> or <%: Html.ActionLink("Start now", "quickstart", "registration", new { id = "start-now" })%></span>
            <div class="clear"></div>
            <p>
            If you are just in hurry or have doubts you are free to skip regisration. You can start using application with <a href = "<%: Url.Action("Faq", new { area="", controller="Home"})%>#TempReg">temporary account</a> created for you
            </p>    
            <% using (Html.BeginForm("Register", "Registration", FormMethod.Post, new { id="signup_form" })) { %>
                <div id="PasswordValidationMessage" class="validation-summary-errors"></div>
                <%: Html.ValidationSummary(true, "Registration was unsuccessful. Please correct the errors and try again.")%>
                
                <fieldset>
                    <div id="email-container">
                        <label for="email">Email</label>
                        <%: Html.TextBoxFor(m => m.Email) %>
                    </div>
                    <div id="password-container">
                        <label for="password">Password</label>
                        <%: Html.TextBoxFor(m => m.Password, new { type = "password" })%>
                    </div>
                    <div id="confirmation-container">
                        <label for="confim">Password confirmation</label>
                        <%: Html.TextBoxFor(m => m.ConfirmPassword, new { type = "password" })%>
                    </div>
                </fieldset>                
                <input id="submit-button" type="submit" value="Signup"/>
            <%} %>

        </div>
    </div>
</body>
</html>