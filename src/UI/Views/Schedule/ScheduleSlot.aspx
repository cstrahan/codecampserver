<%@ Page Language="C#" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.ScheduleSlotView" %>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="CodeCampServer.UI"%>
	<% foreach (SessionForm session in ViewData.Model) { %>
	<div class="session">
		<%Html.RenderPartial("EditSessionLink",session); %>
		<span><a href="<%=Url.Action<SessionController>(c=>c.Index(null),new{session=session.Id})%>"><%=session.Title %></a></span><span>(Room: <%=session.RoomNumber %>)</span>
	</div>			
	<% } %>
