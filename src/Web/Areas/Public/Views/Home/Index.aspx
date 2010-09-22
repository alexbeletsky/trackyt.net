<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <div id="slider">
        <div class="top">
            <div class="bot">
                <div id="slider-holder">
                    <ul>
                        <li>
                            <div class="cl">
                                &nbsp;</div>
                            <div class="slide-info">
                                <h2 class="notext txt-solutions">
                                    Welcome to Tracky.net, Web application that makes you life easier!</h2>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque lorem lacus,
                                    consectetur mattis interdum ac, aliquam nec massa. Cras libero est, con- dimentum
                                    nec porta et, blandit sed dui. Duis magna elit, bibendum non hen- drerit et, volutpat
                                    pharetra sapien.
                                </p>
                                <p>
                                    <a href="#" class="more">Read more</a></p>
                            </div>
                            <div class="cl">
                                &nbsp;</div>
                        </li>
                    </ul>
                </div>
                <div class="slider-controls">
                    <a href="#">1</a> <a href="#">2</a> <a href="#">3</a> <a href="#">4</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
