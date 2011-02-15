<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/AdminUserManagement.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.List<User>>" %>
<%@ Import Namespace="Trackyt.Core.DAL.DataModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="table-container">
        <table>
            <tr>
                <th>Id</th><th>Email</th><th>Temp</th>    
            </tr>

            <% foreach (var user in Model)
               { %>
                    <tr>
                        <td>
                            <%: user.Id %>                        
                        </td>
                        <td>
                            <%: user.Email %>
                        </td>
                        <td>
                            <%: user.Temp %>
                        </td>
                    </tr>
            <% } %>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
