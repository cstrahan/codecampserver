<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<string[]>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>
    <div class="addMeeting"><%Html.RenderPartial("AddMeetingLink"); %></div>
<% foreach (string eventKey in Model){%>
    <div class="post"> 
			<%Html.RenderAction("announcement", "event", new { @event = eventKey }); %>
		</div> 
		<hr /> 
<%}%>
