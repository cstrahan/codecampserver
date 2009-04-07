<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
	Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<ScheduleForm[]>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
	<h2><%=ViewData.Get<PageInfo>().SubTitle%> / Schedule</h2>
	<table class="schedule">
	<%--Loop through each ScheduleForm and output a schedule table
		for that day of the conference--%>
	<% foreach (var scheduleForm in Model) { %>
		<%--header row--%>
		<tr class="headerrow">
			<%--Left column--%>
			<th class="day">Day <%=scheduleForm.Day %>, <%=scheduleForm.Date %></th>
			<%--new column for each track of the conference--%>
			<% foreach (var track in scheduleForm.Tracks) { %>
			<th><%=track.Name%> <% Html.RenderPartial("EditTrackLink", track);%></th>
			<% } %>
		</tr>
		<%--new content row for each time slot--%>
		<% foreach (var timeSlotAssignment in scheduleForm.TimeSlotAssignments) { %>
		<tr class="timeslotrow">
			<%--put time slot in the first cell of each row--%>
			<td class="timeslot">
				<%=timeSlotAssignment.TimeSlot.GetName()%> 
				<%--using partials to render administrative links where appropriate--%>
				<% Html.RenderPartial("EditTimeSlotLink", timeSlotAssignment.TimeSlot);%>
			</td>
			<%--new table cell for each time slot and track.  
				All sessions will go in that cell--%>
			<% foreach (var trackAssignment in timeSlotAssignment.TrackAssignments) { %>
			<td>
				<% Html.RenderPartial("ScheduleSlot", trackAssignment.Sessions,ViewData);%>
			</td>
			<% } %>
		</tr>
		<% } %>
	<% } %>	
	</table>
</asp:Content>
