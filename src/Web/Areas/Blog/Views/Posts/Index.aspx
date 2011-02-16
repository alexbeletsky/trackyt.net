<%@ Page Title="Trackyt.net | Product blog" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage<Web.Areas.Blog.Models.BlogPosts>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div id="blogpost-container">

            <% if (Model.CurrentPage != 1)
           {
               %>
               <%: Html.Partial("Pager") %>
               <%
           } 
             %>

            <% foreach (var blogPost in Model.Content)
               { %>
                    <div class="blogpost">
                        <div class="header">
                            <div class="title">
                                <%: Html.ActionLink(blogPost.Title, "PostByUrl", new { url = blogPost.Url })%>
                               </div>
                            <div class="posted-on">
                                Published on: <%: blogPost.CreatedDate.ToShortDateString() %> 
                                By: <a href="<%: blogPost.Site %>"><%: blogPost.CreatedBy %></a>
                            </div>
                            <div class="comments">
                                <a href="<%: Url.Action("PostByUrl", new { url = blogPost.Url }) %>#disqus_thread" data-disqus-identifier="<%: "disqus_id_" + blogPost.Id %>"></a>
                            </div>
                        </div>
                        <div class="body">
                            <%: MvcHtmlString.Create(blogPost.Body) %>
                        </div>
                        <div class="footer">
                        </div>
                    </div> 
            <% } %>

            <%: Html.Partial("Pager") %>

            <!-- Disqus -->
            <script type="text/javascript">
                /* * * CONFIGURATION VARIABLES: EDIT BEFORE PASTING INTO YOUR WEBPAGE * * */
                var disqus_shortname = 'trackytnetproductblog'; // required: replace example with your forum shortname

                /* * * DON'T EDIT BELOW THIS LINE * * */
                (function () {
                    var s = document.createElement('script'); s.async = true;
                    s.type = 'text/javascript';
                    s.src = 'http://' + disqus_shortname + '.disqus.com/count.js';
                    (document.getElementsByTagName('HEAD')[0] || document.getElementsByTagName('BODY')[0]).appendChild(s);
                } ());
            </script>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
