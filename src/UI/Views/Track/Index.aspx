<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.TrackIndex" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<%@ Import Namespace="CodeCampServer.UI"%>

<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
	<%  Html.RenderPartial("AdminMenu");%>
	
	<% foreach (var track in ViewData.Model) { %>
		
		<p class="trackname"><a href="<%= Url.Action<TrackController>(t => t.Edit(null), new{track = track.Id}).ToXHTMLLink() %>"><%= track.Name %></a></p>
	
	<% } %>
	
</asp:Content>
