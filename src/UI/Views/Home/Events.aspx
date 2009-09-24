<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
    Inherits="ViewPage<UserGroupForm>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">

    <script type="text/javascript" src="/scripts/rsswidget.js"></script>

</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <%Html.RenderAction("List", "Event", ViewContext.RouteData.DataTokens);%>    
</asp:Content>
