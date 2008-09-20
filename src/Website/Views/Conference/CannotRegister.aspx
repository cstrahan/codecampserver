<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" 
AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage"
 Title="Register for conference" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import namespace="MvcContrib"%>
<%@ Import namespace="CodeCampServer.Model.Presentation"%>
<%@ Import namespace="CodeCampServer.Website.Controllers"%>
<%@ Import namespace="CodeCampServer.Model.Domain"%>

<asp:Content ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div>
	    Registrations are now closed for <%= getConference().Name %>.
	</div>
</asp:Content>

<asp:Content ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>

<script runat="server">
	private Schedule getConference()
	{
		Schedule conference = ViewData.Get<Schedule>();
		return conference;
	}

</script>
