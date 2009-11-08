<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<string[]>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>
    <div class="addMeeting"><%Html.RenderPartial("AddMeetingLink"); %></div>
<% foreach (string eventKey in Model){%>
    <div class="post"> 
			<%ViewExtensions.RenderAction(Html, "announcement", "event", new { @event = eventKey }); %>
		</div> 
		<hr /> 
<%}%>
