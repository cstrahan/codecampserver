<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
    Inherits="ViewPage<UserGroupInput>" %>
<%@ Import Namespace="CodeCampServer.Core"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">

    <script type="text/javascript" src="/scripts/rsswidget.js"></script>

</asp:Content>
<asp:Content ContentPlaceHolderID="Main" runat="server">
	
    <%ViewExtensions.RenderAction(Html, "List", "Event", ViewContext.RouteData.DataTokens);%>    
</asp:Content>
