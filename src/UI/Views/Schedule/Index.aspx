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
	
	<table>
		<tr>
			<th></th>
			<% foreach (var track in ViewData.Model.Tracks) { %>
			<th><%=track.Name%></th>
			<% } %>
		</tr>
		<% foreach (var timeSlotAssignment in ViewData.Model.TimeSlotAssignments) { %>
		<tr>
			<td><%=timeSlotAssignment.TimeSlot.GetName()%></td>
			<% foreach (var trackAssignment in timeSlotAssignment.TrackAssignments) { %>
			<td><% Html.RenderPartial("ScheduleSlot", trackAssignment.Sessions);%></td>
			<% } %>
		</tr>
		<% } %>
	</table>	
</asp:Content>
