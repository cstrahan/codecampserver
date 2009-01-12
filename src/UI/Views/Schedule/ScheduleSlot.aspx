<%@ Page Language="C#" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.ScheduleSlotView" %>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>

	<% foreach (SessionForm session in ViewData.Model) { %>
	<div class="fl m5">
		<%=session.Title %>
	</div>			
	<% } %>
