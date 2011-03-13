<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Web.Areas.Admin.Models.AdminLogin>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Admin | Login</title>
    <link href="../../../../Content/admin-login.css?ver=1010" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="admin-login">
        <div class="admin-login-box">
            <% using(Html.BeginForm("login", "login")) { %>
                <%: Html.TextBoxFor(m => m.Password, new { type = "password" })%>

                <input id="login" value="Login" type="submit" />

            <%
               }
            %>
        </div>
    </div>
</body>
</html>
