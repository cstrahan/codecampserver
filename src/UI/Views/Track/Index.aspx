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
<p><a href="<%=Url.Action<TrackController>(x => x.New(null)).ToXHTMLLink() %>">Add a new track</a></p>
	<% foreach (var track in ViewData.Model) { %>
		
		
		<div class="w200">
		<div class="fl pr15"><a title="delete" href="<%= Url.Action<TrackController>(t => t.Delete(null), new{track = track.Id}).ToXHTMLLink() %>"><img src="<%= Url.Content("~/images/icons/delete.png").ToXHTMLLink() %>" /></a></div>
		<div><a href="<%= Url.Action<TrackController>(t => t.Edit(null), new{track = track.Id}).ToXHTMLLink() %>"><%= track.Name %></a></div>
		
		</div>
	
	<% } %>
	
</asp:Content>
