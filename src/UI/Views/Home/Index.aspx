<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<UserGroupInput>" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="CodeCampServer.Core"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
    <script type="text/javascript" src="/scripts/rsswidget.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
<% TempData.Select(pair => pair.Value).ForEach(o => Writer.Write(o)); %>
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
