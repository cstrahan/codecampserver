<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.ScheduleView" %>
<%@ Import Namespace="CodeCampServer.Core.Common"%>
<%@ Import Namespace="CodeCampServer.UI.Controllers"%>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<%@ Import Namespace="CodeCampServer.UI"%>

<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
	<h2>Schedule</h2>
	<table class="schedule">
	<% foreach (var scheduleForm in ViewData.Model) { %>
		<tr class="headerrow">
			<th class="day">Day <%=scheduleForm.Day %>, <%=scheduleForm.Date %></th>
			<% foreach (var track in scheduleForm.Tracks) { %>
			<th><%=track.Name%> <% Html.RenderPartial("EditTrackLink", track);%></th>
			<% } %>
		</tr>
		<% foreach (var timeSlotAssignment in scheduleForm.TimeSlotAssignments) { %>
		<tr class="timeslotrow">
			<td class="timeslot"><%=timeSlotAssignment.TimeSlot.GetName()%> <% Html.RenderPartial("EditTimeSlotLink", timeSlotAssignment.TimeSlot);%></td>
			<% foreach (var trackAssignment in timeSlotAssignment.TrackAssignments) { %>
			<td><% Html.RenderPartial("ScheduleSlot", trackAssignment.Sessions);%></td>
			<% } %>
		</tr>
		<% } %>
	<% } %>	
	</table>
</asp:Content>
