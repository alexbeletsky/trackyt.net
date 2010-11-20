<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <h1>Trackyt.net Api documentation</h1>
            <h2>Current API version</h2>
            <p>
            Current version of API is v1.
            </p>
            <h2>Authentication</h2>
            <p>
            Before usage of any API methods, you need to authenticate yourself. This can be done via HTTP POST. After successful authentication a session is created using a cookie.

If authentication fails, HTTP status code 403 is returned.
            </p>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Trackyt.net | API</title>
</asp:Content>
