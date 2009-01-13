<%@ Page Language="C#" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.ScheduleSlotView" %>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>

	<% foreach (SessionForm session in ViewData.Model) { %>
	<div class="session">
		<%=session.Title %>
	</div>			
	<% } %>
