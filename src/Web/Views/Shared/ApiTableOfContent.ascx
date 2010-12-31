<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
    <div class="content">
        <h1>Trackyt.net Api documentation</h1>
            <p>Current version of API is v1.1</p>
            <p style="font-size: x-small;">Older versions of API are also supported, but it is highly recommended to use the latest one.</p>
            <div class="center">
                <%: Html.ActionLink("v.1.1", "ApiV11", new { area = "", controller = "Home" }) %> | 
                <%: Html.ActionLink("v.1.0", "ApiV10", new { area = "", controller = "Home" }) %> 
            </div>
    </div>
