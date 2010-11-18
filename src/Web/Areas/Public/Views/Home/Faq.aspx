<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Public/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <h2>Frequently asked questions</h2>
        <ul class="faq-list">
            <li> <a href="#WhatIsTrackyt">What is Trackyt.net?</a></li>
            <li> <a href="#WhoCanUse">Who can use Trackyt.net?</a></li>
            <li> <a href="#IsItFree">Is it free?</a></li>
            <li> <a href="#TempReg">What is temporary registration?</a></li>
            <%--Should be enabled as soon as feedback chanell available--%>
            <%--<li> <a href="#FoundABug">I found a bug, what can I do?</a></li>--%>
            <li> <a href="#Api">Do you have an API?</a></li>
            <li> <a href="#TechBehind">What technologies behind Trackyt.net?</a></li>
            <li> <a href="#OpenSource">Are you open source?</a></li>
            <li> <a href="#Help">I want to help this project, what can I do?</a></li>
        </ul>

        <div class="grey-gradient out-box">
            <ul class="faq-ans">
                <li id="WhatIsTrackyt">
                    <strong>What is Trackyt.net?</strong>
                    <p>
trackyt.net is a simple, small and smart web application. Its primary goal is to provide you with a convenient tools for tasks and time management. Now, it is individual solution that helps you create, prioritize and track how much do you spend on every task you need to work on. It is perfect for freelances, organizations employees and anyone who cares about planning and self-organization.
                    </p>
                </li>
                <li id="WhoCanUse">
                    <strong>Who can use Trackyt.net?</strong>
                    <p>
                    Anyone who is interested in his productivity. It best matches for freelances, developers, designers who need to plan their tasks and know effort spent on this task.
                    </p>
                </li>
                <li id="IsItFree">
                    <strong>Is it free?</strong>
                    <p>
                    Yes, it is absolutely free for personal usage.
                    </p>
                </li>
                <li id="TempReg">
                    <strong>What is temporary registration?</strong>
                    <p>
                    Is a just a simple way for you to try application. By choosing 'Start now' during registration, we will create new account for you. Email and password would be generated automatically, so it is very important to save them during first login. Account information is available on dashboard by clicking on 'Account' link. Please not, that we will just keep you data for 30 days, after that you should create permanent account.
                    </p>
                </li>
                <li id="Api">
                    <strong>Do you have an API?</strong>
                    <p>
                    Yes. Trackyt.net is build on REST architecture and you are able to use it.
                    </p>
                </li>
                <li id="TechBehind">
                    <strong>What technologies behind Trackyt.net?</strong>
                    <p>
                    Trackyt.net is <a href="http://www.asp.net/mvc">ASP.net MVC 2</a> application with a lot of Javascript.
                    </p>
                    <p>It also utilizes many open source products</p>
                    <ul class="list">
                        <li><a href="http://automapper.codeplex.com/">AutoMapper</a></li>
                        <li><a href="http://code.google.com/p/moq/wiki/QuickStart">moq</a></li>
                        <li><a href="http://nant.sourceforge.net/">Nant</a></li>
                        <li><a href="http://ninject.org/">Ninject</a></li>
                        <li><a href="http://www.nunit.org/">NUnit</a></li>
                        <li><a href="http://www.testdriven.net/">Testdriven.net</a></li>
                        <li><a href="http://www.json.org/js.html">Json2</a></li>
                        <li><a href="http://jquery.com/">jQuery</a></li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Trackyt.net | FAQ</title>
</asp:Content>
