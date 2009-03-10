<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ScheduleForm[]>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
	<h2>Schedule</h2>
	<table class="schedule">
	<% foreach (var scheduleForm in Model) { %>
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
			<td><% Html.RenderPartial("ScheduleSlot", trackAssignment.Sessions,ViewData);%></td>
			<% } %>
		</tr>
		<% } %>
	<% } %>	
	</table>
</asp:Content>
