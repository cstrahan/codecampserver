<%@ Page Language="C#" AutoEventWireup="true" Title="Schedule" 
    Inherits="System.Web.Mvc.ViewPage" 
    MasterPageFile="~/Views/Layouts/Default.Master" %>
<%@ Import Namespace="CodeCampServer.Website.Controllers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="CodeCampServer.Model.Presentation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" 
    runat="server">
	<div style="padding: 10px">
        <% Schedule conference = ViewData.Get<Schedule>(); %>
		<% TimeSlot timeslot = ViewData.Get<TimeSlot>(); %>
		<% Track track = ViewData.Get<Track>(); %>
		<% Session[] allocatedSessions = ViewData.Get<Session[]>("AllocatedSessions"); %>
		<% Session[] unallocatedSessions = ViewData.Get<Session[]>("UnallocatedSessions"); %>
		<%= Html.ActionLink<ScheduleController>((c => c.Index(conference.Key)), "« Session List")%>
		<h1>
		    Edit Session
		    <br />
			<%= track.Name %> Track
			<br />
			<%= timeslot.StartTime.ToString("hh:mm tt") %>-<%= timeslot.EndTime.ToString("hh:mm tt")%></h1>	
		<h3>Allocated Sessions</h3>
		<ul>
		    <% foreach (Session session in allocatedSessions) { %>
		        <li>
		            <%= session.Title %>
		            <% var sessionId = session.Id; %>
		            <%= Html.ActionLink<ScheduleController>((c => c.RemoveSession(conference.Key, track.Id, timeslot.Id, sessionId)), "Remove")%>
		        </li>
		    <%} %>
		</ul>
		
		<h3>Unallocated Sessions</h3>
		<ul>
		    <% foreach (Session session in unallocatedSessions) { %>
		        <li>
		            <%= session.Title %>
		            <% var sessionId = session.Id; %>
		            <%= Html.ActionLink<ScheduleController>((c => c.AddSession(conference.Key, track.Id, timeslot.Id, sessionId)), "Add")%>    
		        </li>
		    <%} %>
		</ul>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" 
    runat="server">
</asp:Content>
