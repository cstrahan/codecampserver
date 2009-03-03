<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>

<%@ Import Namespace="CodeCampServer.UI.Controllers"%>

<%
	var track = (TrackForm) ViewData.Model; %>
		<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
				<a title="Edit <%=track.Name%>" href="<%= Url.Action<TrackController>(t => t.Edit(null), new{track = track.Id}).ToXHTMLLink() %>"><img src="/images/icons/application_edit.png" /></a>
		<%}%>		
