<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="navigation">
    <ul class="right">
        <li class="login">
            <%: Html.ActionLink("Sign In", "Index", new { area = "", controller = "Login" })%></li>
        <%--                    <li class="registration"><%: Html.ActionLink("Sign Up", "Index", new { area = "", controller = "Registration" })%></li>--%>
        <%-- <li class="support"><a href="#"></a></li>--%>
    </ul>
    <ul class="left">
        <li class="home">
            <%: Html.ActionLink("Home", "Index", new { area = "", controller = "Home" })%></li>
        <%--                    <li class="product-tour"><a href="#"></a></li>--%>
        <li class="blog">
            <%: Html.ActionLink("Blog", "Index", new { area = "Blog", controller = "Posts" }) %></li>
        <%--    <li class="about-us"><a href="#"></a></li>--%>
    </ul>
</div>
<div class="navigation-line">
</div>
