<%@ Page Language="C#" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.ScheduleSlotView" %>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>

	<% foreach (SessionForm session in ViewData.Model) { %>
	<div class="session">
		<%Html.RenderPartial("EditSessionLink",session,ViewData); %>
		<span><a href="<%=Url.RouteUrl("session",new {sessionKey=session.Key})%>"><%=session.Title %></a></span><span>(Room: <%=session.RoomNumber %>)</span>
	</div>			
	<% } %>
