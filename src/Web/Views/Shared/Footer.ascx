<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div id="navigation-footer">
    <div class="navigation-footer-block left">
        <ul>
            <li class="home">
                <%: Html.ActionLink("home", "Index", new { area = "", controller = "Home" })%></li>
            <li class="product-tour"><a href="#">product tour</a></li>
            <li class="blog">
                <%: Html.ActionLink("blog", "Index", new { area = "Blog", controller = "Posts" }) %></li>
            <%--                <li class="about-us"><a href="#">about us</a></li>--%>
        </ul>
    </div>
    <div class="navigation-footer-block left">
        <ul>
            <li class="register bold">
                <%: Html.ActionLink("register now!", "Index", new { area = "", controller = "Registration" })%></li>
            <li class="login">
                <%: Html.ActionLink("login", "Index", new { area = "", controller = "Login" })%></li>
            <%--<li class="support"><a href="#">support</a></li>--%>
        </ul>
    </div>
    <div class="navigation-footer-block left">
        <ul>
            <li class="api bold">
                <%: Html.ActionLink("api", "ApiV11", new { area = "", controller = "Home" })%></a></li>
            <li class="opensource"><a href="<%: Url.Action("Faq", new { area="", controller="Home"}) %>#OpenSource">
                open source</a></li>
            <li class="faq">
                <%: Html.ActionLink("faq", "Faq", new { area = "", controller = "Home" })%></li>
        </ul>
    </div>
    <div class="navigation-footer-block right">
        <ul>
            <li class="twitter bold"><a href="http://twitter.com/#!/trackytnet">@trackytnet </a>
            </li>
        </ul>
    </div>
</div>

