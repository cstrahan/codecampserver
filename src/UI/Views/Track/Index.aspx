<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.TrackIndexView" %>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<%@ Import Namespace="CodeCampServer.UI"%>

<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
<h2>Tracks
		<%if (User.Identity.IsAuthenticated){%>
				<a class="" href="<%=Url.Action<TrackController>(c=>c.New(null))%>" title="Add a new Track"><img src="/images/icons/application_add.png" /></a>
		<%}%>
</h2>
<p>
	<% foreach (var track in ViewData.Model) { %>
		<div class=" w250 ">
			<div class="fl"><a href="<%= Url.Action<TrackController>(t => t.Edit(null), new{track = track.Id}).ToXHTMLLink() %>"><%= track.Name %></a></div>
			<div class="fr pr15"><a title="delete" href="<%= Url.Action<TrackController>(t => t.Delete(null), new{track = track.Id}).ToXHTMLLink() %>"><img src="<%= Url.Content("~/images/icons/delete.png").ToXHTMLLink() %>" /></a></div>
			<div class="fr pr15"><a title="edit" href="<%= Url.Action<TrackController>(t => t.Edit(null), new{track = track.Id}).ToXHTMLLink() %>"><img src="<%= Url.Content("~/images/icons/application_edit.png").ToXHTMLLink() %>" /></a></div>
		</div>
	<% } %>	
</p>
</asp:Content>
