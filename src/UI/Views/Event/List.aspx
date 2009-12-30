<%@ Page Language="C#" AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.BaseViewPage<string[]>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>
    <div class="addMeeting"><%Html.RenderPartial("AddMeetingLink"); %></div>
<% foreach (string eventKey in Model){%>
    <div class="post"> 
		<%Html.RenderAction("announcement", "event", new { @event = eventKey }); %>
	</div> 
	<hr /> 
<%}%>
