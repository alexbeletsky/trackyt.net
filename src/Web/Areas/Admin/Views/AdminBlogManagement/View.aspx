<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/AdminBlogManagement.Master" Inherits="System.Web.Mvc.ViewPage<Trackyt.Core.DAL.DataModel.BlogPost>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Model.Title %></h2>
    <h5>
    Published on: <%: Model.CreatedDate.ToShortDateString()%> By: <%: Model.CreatedBy%>
    </h5>
    <div>
    <%: MvcHtmlString.Create(Model.Body)%>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
