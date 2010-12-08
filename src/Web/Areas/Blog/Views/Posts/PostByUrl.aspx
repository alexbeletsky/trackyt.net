<%@ Page Title="Trackyt.net | Product blog" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <% if (Model == null)
           {
                %>

                <p>Sorry, but requested document has not been found!</p>

                <% 
           }
           else
           {
               %>
               <div id="blogpost-container">
                    <div class="blogpost">
                        <div class="header">
                            <div class="title">
                                <%: Model.Title %>
                            </div>
                            <div class="posted-on">
                                Published on: <%: Model.CreatedDate.ToShortDateString()%> By: <%: Model.CreatedBy%>
                            </div>
                        </div>
                        <div class="body">
                            <%: MvcHtmlString.Create(Model.Body)%>
                        </div>
                        <div class="footer">
                        </div>
                    </div> 
                </div>

               <%
           }
        %>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
