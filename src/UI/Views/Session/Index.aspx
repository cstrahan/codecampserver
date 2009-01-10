<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.SessionIndexView" %>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<%@ Import Namespace="CodeCampServer.UI"%>

<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
	<%  Html.RenderPartial("AdminMenu");%>
	
	<% foreach (var session in ViewData.Model) { %>
		
		<p class="sessionname"><a href="<%= Url.Action<SessionController>(t => t.Edit(null), new{session = session.Id}).ToXHTMLLink() %>"><%= session.Title%></a>
		&nbsp;
		<a href="<%= Url.Action<SessionController>(t => t.Delete(null), new{session = session.Id}).ToXHTMLLink() %>"><img src="/images/Buttons/delete_icon.gif" /></a></p>
	
	<% } %>
	
</asp:Content>
