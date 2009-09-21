<%@ Import Namespace="CodeCampServer.Core.Domain.Model"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>

<% var conference = ViewData.Get<Conference>(); %>
<div id="menu"> 
    <ul> 
		<asp:LoginView ID="LoginView1" runat="server">
			<LoggedInTemplate>
				<li><a href="<%=Url.Action<AdminController>(c=>c.Index(null)) %>">admin</a></li>
			</LoggedInTemplate>
		</asp:LoginView>

		<li><a href="<%=Url.RouteUrl("conferencedefault",new{controller="conference",action="index",conferencekey=conference.Key}) %>">home</a></li>
		<li><a href="<%=Url.Action<ScheduleController>(c=>c.Index(null)) %>">schedule</a></li>
		<li><a href="<%=Url.Action<SpeakerController>(c=>c.List(null)) %>">speakers</a></li>
		<li><a href="<%=Url.Action<SessionController>(c=>c.List(null)) %>">sessions</a></li>

		<%if (conference.HasRegistration)
		{ %>
		<li><a href="<%=Url.Action<AttendeeController>(c=>c.Index(null)) %>">attendees</a></li>
		<li><a href="<%=Url.Action<AttendeeController>(c=>c.New(null)) %>">register</a></li>
		<% } %>
		
		<li><a href="<%=Url.Action<ProposalController>(c=>c.List(null)) %>">Session&nbsp;Proposals</a></li>
		
	</ul>
</div>
