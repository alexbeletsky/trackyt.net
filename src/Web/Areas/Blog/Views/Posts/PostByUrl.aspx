<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Public/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% if (Model == null)
       {
            %>

            <p>Sorry, but requested document has not been found!</p>

            <% 
       }
       else
       {
           %>

            <div class="blogpost">
                <div class="header">
                    <div class="title">
                        <%: Model.Title %>
                    </div>
                    <div class="date">
                        Published on: <%: Model.CreatedDate.ToShortDateString()%>
                    </div>
                    <div class="createdby">
                        By: <%: Model.CreatedBy%>
                    </div>
                </div>
                <div class="body">
                    <%: MvcHtmlString.Create(Model.Body)%>
                </div>
                <div class="footer">
                </div>
            </div> 

           <%
       }
    %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
