<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
    Inherits="ViewPage<UserGroupForm>" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
    <script type="text/javascript" src="/scripts/rsswidget.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <%=Html.ValidationSummary()%>
    <%Html.RenderAction("UpComing", "Event", ViewContext.RouteData.DataTokens);%>
</asp:Content>
<asp:Content ContentPlaceHolderID="SidebarPlaceHolder" runat="server">
    <%Html.RenderPartial("Sponsors", Model.Sponsors);%>
    <hr />
    <h2>
        <%= Model.Name %>
        <%Html.RenderPartial("EditUserGroupLink", Model); %></h2>
    <p>
        <%= Model.Location() %>
        </p>
</asp:Content>
