<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/AdminBlogManagement.Master" Inherits="System.Web.Mvc.ViewPage<Trackyt.Core.DAL.DataModel.BlogPost>" validateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add new post to product blog</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        <% if (Model != null && Model.Id != 0)
           { %>
                <div id="summary-container">
                    <span>Congratulations! New blog post has been <span class="green">successfully</span> created. 
                    <%: Html.ActionLink("View post", "PostByUrl", new { area = "Blog", controller = "Posts", url = Model.Url })%>
                    </span>
                </div>
        <% }%>
        <fieldset>
            <legend>Post details</legend>
                        
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Title) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Title) %>
                <%: Html.ValidationMessageFor(model => model.Title) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Body) %>
            </div>
            <div class="editor-field">
                <%: Html.TextAreaFor(model => model.Body) %>
                <%: Html.ValidationMessageFor(model => model.Body) %>
            </div>
                        
            <div class="editor-label">
                <%: Html.LabelFor(model => model.CreatedBy) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.CreatedBy) %>
                <%: Html.ValidationMessageFor(model => model.CreatedBy) %>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(model => model.Site) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Site) %>
                <%: Html.ValidationMessageFor(model => model.Site) %>
            </div>
            
            <p>
                <input type="submit" value="Post" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

