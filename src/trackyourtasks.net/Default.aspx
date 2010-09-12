<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="trackyourtasks.net._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="js/tasksgrid.js" type="text/javascript"></script>
    <script type="text/javascript">
        //        $().ready(function () {
        //            var ser = new Services.TasksService();
        //            var table = $("#tasks").tasksgrid(loadData, submitData);

        //            function loadData(callback) {
        //                return ser.GetAllTasks(callback, null, null);
        //            }

        //            function submitData(data, callback) {
        //                return ser.Submit(data, callback, null, null);
        //            }
        //        }
        //      );
        //
        $().ready(function () {
        }
        ); 
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <services>
            <asp:ServiceReference Path="Services/TasksService.svc" />
        </services>
    </asp:ScriptManager>

    <table id="tasks">
        <thead>
        </thead>
        <tbody>
        </tbody>
    </table>
    --%>
    <!-- Header -->
    <div id="header">
        <!-- Slider -->
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
        <!-- END Slider -->
    </div>
    <!-- END Header -->
    
    <!-- END Main -->
    <%--    <select id="1">
        <option text="xx" value="1" selected="selected"></option>
        <option label="xxx" value="1" selected="false"></option>
        <option label="xxxx" value="1" selected="true"></option>
    </select>
    --%>
</asp:Content>
