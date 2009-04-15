<%@ Import Namespace="CodeCampServer.Core.Domain.Model"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>

<% var conference = ViewData.Get<Conference>(); %>

<div id="navigationWrapper">
	<div id="leftNavigationEndCap"></div>
	<ul id="navigationMenu">
		<asp:LoginView ID="LoginView1" runat="server">
			<LoggedInTemplate>
				<li><a href="<%=Url.Action<AdminController>(c=>c.Index(null)) %>"><img src="~/images/themes/green/buttons/admin.gif" runat="server" id="admin" alt="Admin" /></a></li>
			</LoggedInTemplate>
		</asp:LoginView>

		<li><a href="<%=Url.RouteUrl("conferencedefault",new{controller="conference",action="index",conferencekey=conference.Key}) %>"><img src="~/images/themes/green/buttons/home.gif" runat="server" id="home" alt="Home" /></a></li>
		<li><a href="<%=Url.Action<ScheduleController>(c=>c.Index(null)) %>"><img src="~/images/themes/green/buttons/schedule.gif" runat="server" id="schedule" alt="Schedule" /></a></li>
		<li><a href="<%=Url.Action<SpeakerController>(c=>c.List(null)) %>"><img src="~/images/themes/green/buttons/speakers.gif" runat="server" id="speakers" alt="Speakers" /></a></li>
		<li><a href="<%=Url.Action<SessionController>(c=>c.List(null)) %>"><img src="~/images/themes/green/buttons/sessions.gif" runat="server" id="sessions" alt="Sessions" /></a></li>

		<%if (conference.HasRegistration)
		{ %>
		<li><a href="<%=Url.Action<AttendeeController>(c=>c.Index(null)) %>"><img src="~/images/themes/green/buttons/attendees.gif" runat="server" id="attendees" alt="Attendees" /></a></li>
		<li><a href="<%=Url.Action<AttendeeController>(c=>c.New(null)) %>"><img src="~/images/themes/green/buttons/register.gif" runat="server" id="register" alt="Register" /></a></li>
		<% } %>
		
		<!--<li><a href="<%=Url.Action<ProposalController>(c=>c.List(null)) %>">Session&nbsp;Proposals</a></li>-->
		
	</ul>
	<div id="rightNavigationEndCap"></div>
</div>
<div class="cleaner"></div>