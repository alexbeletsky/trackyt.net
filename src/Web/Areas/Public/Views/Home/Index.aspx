<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Public/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <div id="screenshot">
        <div id="slogan">
            Track Your Tasks on .NET
        </div>
        <div id="info">
            <p class="dark big">
                Simple.Small.Smart.
            </p>
            <div class="green-button bottom">
                <%: Html.ActionLink("Sign up for free", "Index", new { area = "Public", controller = "Registration" })%>
                <p>
                    No registration needed
                </p>
            </div>
        </div>
    </div>

    <div class="content">
        <p>
            <strong>trackyt.net</strong> is a simple, small and smart web application. Its primary goal is to provide you with a convenient tools for tasks and time management. Now, it is individual solution that helps you create, prioritize and track how much do you spend on every task you need to work on. It is perfect for freelances, organizations employees and anyone who cares about planning and self-organization.
        </p>
        <div class="grey-gradient out-box">    
        <div class="column">
            <h2>Why to use it?</h2>
            <p>
                As it said in our slogan, we tried to build simple, small and smart tool to you. Simplicity is our primary criteria for building software - you don't have to crawl thought documentation, instead you just run and use! We want to stay small, meaning to create exactly what you need. Our goal is to collect as much more feedbacks as we can and return it as features for product. We want to make smart things that also helps you to solve your problems.
            </p>
        </div>
        <div class="column">
            <h2>What's your plans?</h2>
            <p>
                We are continuously improve the product. We have a long term product plans and you could familiarize with current version of <a href="https://github.com/alexanderbeletsky/Trackyourtasks.net/wiki/Project-Roadmap-2010---2011">Product Roadmap</a>. Roadmap shows major releases we are going to do thought the year. We work in short iterations to always deliver something, so product just is about to grow.
            </p>            
        </div>
        <div class="column">
            <h2>Anything more?</h2>
            <p>
                Product is build using <a href="http://en.wikipedia.org/wiki/Representational_State_Transfer">REST</a> architecture and we really much care about extensibilty. We work on API that allows any third-party vendors to integrate. It also provides abilities to build new applications that uses existing functionality created for trackyt.net. This is a big area for improvements for mobile applications. 
            </p>
        </div>
        </div>
        <div class="clear"></div>
        <p>
            As our user and customer, make your can suggestions or problem visible for us. In our turn we'll do the best to match our vision with yours expectations. Create your account or start temporary one and boost your productivity and time management with <strong>trackyt.net</strong>
        </p>
        <p class="signature">
            trackyt.net Development Team
        </p>
        <div class="clear"></div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
