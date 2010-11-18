<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Public/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <div id="screenshot">
        <div id="slogan">
            Track Your Tasks on .NET
        </div>
        <div id="info">
            <p class="dark big">
                Beautiful.Usefull.Small
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
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis auctor leo. Nam tempor, sem quis tincidunt pulvinar, turpis magna pretium eros, in iaculis libero nisi vitae ante. In hac habitasse platea dictumst. Phasellus laoreet, lorem vel pretium tempor, dolor velit vestibulum libero, at gravida erat nisl vitae lacus. Cras porta, sapien id porttitor consectetur, lorem purus feugiat nunc, vel imperdiet est metus sed quam. Cras in augue ac metus hendrerit tempor. Donec ac nisi felis, ac sodales risus. Nunc viverra molestie ante quis convallis
        </p>
        <div class="grey-gradient out-box">    
        <div class="column">
            <h2>Why trackyt?</h2>
            <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis auctor leo. Nam tempor, sem quis tincidunt pulvinar, turpis magna pretium eros, in iaculis libero nisi vitae ante. In hac habitasse platea dictumst. Phasellus laoreet, lorem vel pretium tempor, dolor velit vestibulum libero, at gravida erat nisl vitae lacus. Cras porta, sapien id porttitor consectetur, lorem purus feugiat nunc, vel imperdiet est metus sed quam. Cras in augue ac metus hendrerit tempor. Donec ac nisi felis, ac sodales risus. Nunc viverra molestie ante quis convallis
            </p>
        </div>
        <div class="column">
            <h2>What</h2>
            <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis auctor leo. Nam tempor, sem quis tincidunt pulvinar, turpis magna pretium eros, in iaculis libero nisi vitae ante. In hac habitasse platea dictumst. Phasellus laoreet, lorem vel pretium tempor, dolor velit vestibulum libero, at gravida erat nisl vitae lacus. Cras porta, sapien id porttitor consectetur, lorem purus feugiat nunc, vel imperdiet est metus sed quam. Cras in augue ac metus hendrerit tempor. Donec ac nisi felis, ac sodales risus. Nunc viverra molestie ante quis convallis
            </p>            
        </div>
        <div class="column">
            <h2>Where</h2>
                        <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis auctor leo. Nam tempor, sem quis tincidunt pulvinar, turpis magna pretium eros, in iaculis libero nisi vitae ante. In hac habitasse platea dictumst. Phasellus laoreet, lorem vel pretium tempor, dolor velit vestibulum libero, at gravida erat nisl vitae lacus. Cras porta, sapien id porttitor consectetur, lorem purus feugiat nunc, vel imperdiet est metus sed quam. Cras in augue ac metus hendrerit tempor. Donec ac nisi felis, ac sodales risus. Nunc viverra molestie ante quis convallis
            </p>
        </div>
        </div>
        <div class="clear"></div>
        <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis auctor leo. Nam tempor, sem quis tincidunt pulvinar, turpis magna pretium eros, in iaculis libero nisi vitae ante. In hac habitasse platea dictumst. Phasellus laoreet, lorem vel pretium tempor, dolor velit vestibulum libero, at gravida erat nisl vitae lacus. Cras porta, sapien id porttitor consectetur, lorem purus feugiat nunc, vel imperdiet est metus sed quam. Cras in augue ac metus hendrerit tempor. Donec ac nisi felis, ac sodales risus. Nunc viverra molestie ante quis convallis
        </p>

    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
