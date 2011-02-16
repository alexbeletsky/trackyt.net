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
                
                <!-- Disqus -->    
                <div id="disqus_thread"></div>
                <script type="text/javascript">

                    //var disqus_developer = 1;

                    /* * * CONFIGURATION VARIABLES: EDIT BEFORE PASTING INTO YOUR WEBPAGE * * */
                    var disqus_shortname = 'trackytnetproductblog'; // required: replace example with your forum shortname

                    // The following are highly recommended additional parameters. Remove the slashes in front to use.
                    var disqus_identifier = '<%: "disqus_id_" + Model.Id %>';
                    var disqus_url = '<%: Request.Url %>';

                    /* * * DON'T EDIT BELOW THIS LINE * * */
                    (function () {
                        var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;
                        dsq.src = 'http://' + disqus_shortname + '.disqus.com/embed.js';
                        (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
                    })();
                </script>
                <noscript>Please enable JavaScript to view the <a href="http://disqus.com/?ref_noscript">comments powered by Disqus.</a></noscript>
                <a href="http://disqus.com" class="dsq-brlink">blog comments powered by <span class="logo-disqus">Disqus</span></a>

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

               <%
           }
        %>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
