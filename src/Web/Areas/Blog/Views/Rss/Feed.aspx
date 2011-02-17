<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IList<Trackyt.Core.DAL.DataModel.BlogPost>>" %>
<rss version="2.0" xmlns:atom="http://www.w3.org/2005/Atom">
<channel>
<title>trackyt.net product blog</title>
<link>http://<%: Request.Url.Host + Url.Action("Index", new { area="Blog", controller = "Posts" }) %></link>
<description>Blog RSS feed for trackyt.net</description>
<lastBuildDate><%: Model.First().CreatedDate.ToUniversalTime().ToString("r") %></lastBuildDate>
<language>en-us</language>
<% foreach (var p in Model) { %>
    <item>
    <title><%: Html.Encode(p.Title) %></title>
    <link>http://<%: Request.Url.Host +  Url.Action("PostByUrl", new { area="Blog", controller="Posts", url = p.Url }) %></link>
    <guid>http://<%: Request.Url.Host +  Url.Action("PostByUrl", new { area = "Blog", controller = "Posts", url = p.Url })%></guid>
    <pubDate><%: p.CreatedDate.ToUniversalTime().ToString("r") %></pubDate>
    <description><%: p.Body %></description>
    </item>
<% } %>
</channel>
</rss>